﻿using System;
using System.Resources;

using eigenein.SkypeNinja.Core.Resources;

namespace eigenein.SkypeNinja.Core.Common
{
    internal static class Translator
    {
        private static readonly ResourceManager ResourceManager =
            new ResourceManager(typeof(Strings));

        public static string GetString(string name)
        {
            string value = ResourceManager.GetString(name);
            return !String.IsNullOrEmpty(value) ? value : name;
        }
    }
}
