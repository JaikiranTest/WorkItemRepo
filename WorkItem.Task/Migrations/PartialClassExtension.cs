using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WorkItem.Task.Migrations
{
    [KnownType(typeof(WorkItemTaskMigrationExcludeFromCodeCoverage))]
    public partial class WorkItemTaskMigration
    { }

    [KnownType(typeof(WorkItemContextModelSnapshotExcludeFromCodeCoverage))]
    public partial class WorkItemContextModelSnapshot
    { }

    [ExcludeFromCodeCoverage]
    public class WorkItemTaskMigrationExcludeFromCodeCoverage
    { }

    [ExcludeFromCodeCoverage]
    public class WorkItemContextModelSnapshotExcludeFromCodeCoverage
    { }
}
