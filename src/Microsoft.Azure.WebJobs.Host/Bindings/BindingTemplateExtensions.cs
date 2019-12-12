// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Azure.WebJobs.Host.Bindings.Path;
using Microsoft.Azure.WebJobs.Host.Properties;

namespace Microsoft.Azure.WebJobs.Host.Bindings
{
    /// <summary>
    /// Class containing extension methods for <see cref="BindingTemplate"/> and other binding Types.
    /// </summary>
    public static class BindingTemplateExtensions
    {
        /// <summary>
        /// Bind the <see cref="BindingTemplate"/> using the specified binding data.
        /// </summary>
        /// <param name="bindingTemplate">The binding template to validate.</param>
        /// <param name="bindingData">The binding data to apply to the template.</param>
        /// <returns>The bound template string.</returns>
        [Obsolete("Call instance method directly")]
        public static string Bind(this BindingTemplate bindingTemplate, IReadOnlyDictionary<string, object> bindingData)
        {
            if (bindingTemplate == null)
            {
                throw new ArgumentNullException("bindingTemplate");
            }

            return bindingTemplate.Bind(bindingData);
        }
    }
}
