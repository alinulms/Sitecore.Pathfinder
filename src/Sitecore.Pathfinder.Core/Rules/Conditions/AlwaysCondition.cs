﻿// © 2015 Sitecore Corporation A/S. All rights reserved.

using System.Collections.Generic;
using Sitecore.Pathfinder.Rules.Contexts;

namespace Sitecore.Pathfinder.Rules.Conditions
{
    public class AlwaysCondition : ConditionBase
    {
        public AlwaysCondition() : base("always")
        {
        }

        public override bool Evaluate(IRuleContext ruleContext, IDictionary<string, string> parameters)
        {
            return true;
        }
    }
}