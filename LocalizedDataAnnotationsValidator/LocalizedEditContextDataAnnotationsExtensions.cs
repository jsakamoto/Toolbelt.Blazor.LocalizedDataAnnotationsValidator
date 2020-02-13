﻿// This source code is fork from https://github.com/aspnet/AspNetCore/blob/master/src/Components/Forms/src/EditContextDataAnnotationsExtensions.cs
// The .NET Foundation licenses the original file of this forked file to you under the Apache License, Version 2.0.
// See the LICENSE file for the original file of this forked file: https://github.com/aspnet/AspNetCore/blob/master/LICENSE.txt

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Toolbelt.Blazor.Forms.Internals;
using Toolbelt.Blazor.Forms.Internals.Validators;

namespace Toolbelt.Blazor.Forms
{
    public static class LocalizedEditContextDataAnnotationsExtensions
    {
        private static ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> _propertyInfoCache = new ConcurrentDictionary<(Type, string), PropertyInfo>();

        public static EditContext AddLocalizedDataAnnotationsValidation(this EditContext editContext, IStringLocalizerFactory stringLocalizerFactory, string locale = default)
        {
            var localizer = stringLocalizerFactory.Create(editContext.Model.GetType());
            var messages = new ValidationMessageStore(editContext);
            editContext.OnValidationRequested += delegate (object sender, ValidationRequestedEventArgs eventArgs)
            {
                EnforceLocale(locale);
                ValidateModel(localizer, (EditContext)sender, messages);
            };
            editContext.OnFieldChanged += delegate (object sender, FieldChangedEventArgs eventArgs)
            {
                var fieldIdentifier = eventArgs.FieldIdentifier;
                EnforceLocale(locale);
                ValidateField(localizer, editContext, messages, in fieldIdentifier);
            };
            return editContext;
        }

        private static void EnforceLocale(string locale)
        {
            if (!string.IsNullOrEmpty(locale) && CultureInfo.CurrentUICulture.Name != locale)
            {
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(locale);
            }
        }

        private static void ValidateModel(IStringLocalizer localizer, EditContext editContext, ValidationMessageStore messages)
        {
            var validationContext = new LocalizedValidationContext(localizer, editContext.Model);
            var list = new List<ValidationResult>();
            LocalizedValidator.TryValidateObject(editContext.Model, validationContext, list, validateAllProperties: true);
            messages.Clear();
            foreach (var item in list)
            {
                foreach (var memberName in item.MemberNames)
                {
                    var fieldIdentifier = editContext.Field(memberName);
                    messages.Add(in fieldIdentifier, item.ErrorMessage);
                }
            }
            editContext.NotifyValidationStateChanged();
        }

        private static void ValidateField(IStringLocalizer localizer, EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier)
        {
            if (TryGetValidatableProperty(in fieldIdentifier, out var propertyInfo))
            {
                var value = propertyInfo.GetValue(fieldIdentifier.Model);
                var validationContext = new LocalizedValidationContext(localizer, fieldIdentifier.Model);
                validationContext.Context.MemberName = propertyInfo.Name;

                var list = new List<ValidationResult>();
                LocalizedValidator.TryValidateProperty(value, validationContext, list);
                messages.Clear(in fieldIdentifier);
                messages.Add(in fieldIdentifier, list.Select(result => result.ErrorMessage));
                editContext.NotifyValidationStateChanged();
            }
        }

        private static bool TryGetValidatableProperty(in FieldIdentifier fieldIdentifier, out PropertyInfo propertyInfo)
        {
            (Type, string) key = (fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);
            if (!_propertyInfoCache.TryGetValue(key, out propertyInfo))
            {
                propertyInfo = key.Item1.GetProperty(key.Item2);
                _propertyInfoCache[key] = propertyInfo;
            }
            return propertyInfo != null;
        }
    }
}
