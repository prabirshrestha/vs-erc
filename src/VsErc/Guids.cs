// Guids.cs
// MUST match guids.h
using System;

namespace PrabirShrestha.VsErc
{
    static class GuidList
    {
        public const string guidVsErcPkgString = "d4448381-11e8-40b6-974f-d110a8904d79";
        public const string guidVsErcCmdSetString = "746a062c-69f7-41df-8cd8-22dddda9fe2c";

        public static readonly Guid guidVsErcCmdSet = new Guid(guidVsErcCmdSetString);
    };
}