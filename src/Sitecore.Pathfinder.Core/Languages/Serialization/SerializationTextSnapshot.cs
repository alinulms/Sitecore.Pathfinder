﻿// © 2015 Sitecore Corporation A/S. All rights reserved.

using System.ComponentModel.Composition;
using Sitecore.Pathfinder.Diagnostics;
using Sitecore.Pathfinder.Snapshots;

namespace Sitecore.Pathfinder.Languages.Serialization
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SerializationTextSnapshot : TextSnapshot
    {
        [ImportingConstructor]
        public SerializationTextSnapshot([NotNull] ISnapshotService snapshotService) : base(snapshotService)
        {
            Capabilities = SnapshotCapabilities.None;
        }
    }
}
