# Revised Project: Personal Project & Task Management System

The revised project will focus exclusively on projects and tasks. The knowledge base, notes, note search, note-specific indexing, and note-related features are removed. The remaining modules are adjusted to ensure that you still practice the full range of .NET standard library, LINQ, data structures, OOP, persistence, algorithms, and testing skills.

No replacement module is added for the removed functionality. Existing project and task features will provide the learning opportunities instead.

## 1. Project overview

Build a C# console application that lets users organize projects, manage tasks and subtasks, track deadlines and dependencies, analyze productivity, and persist data between application runs.

- Application: Console application
- Language: C#
- Framework: .NET 10 LTS or another supported .NET version
- Initial storage: In-memory collections
- Persistence: JSON files using `System.Text.Json`
- External database: Not required
- Application dependencies: Prefer the .NET BCL; use a test framework for automated tests.

The system has two primary domain entities: `Project` and `TaskItem`. Activity records, task dependencies, and report DTOs support their management without becoming separate product modules.

## 2. Functional requirements and features

### Module A: Project management

Users can create and manage multiple projects, such as learning C#, preparing for interviews, or developing a software application.

Features

1. Create a project with a unique ID, name, description, creation date, and status.
2. Rename, update, archive, and delete projects.
3. List projects with pagination and configurable sorting.
4. Filter projects by status, creation date, and keyword.
5. Calculate completion percentage based on completed tasks.
6. Display pending, in-progress, blocked, overdue, and completed task counts for each project.
7. Prevent project deletion when its tasks or dependencies violate the defined deletion rules.
8. Display project details, associated tasks, subtasks, and recent activity.
9. Find projects with no tasks or no recent activity.
10. Calculate the total estimated and actual time spent on each project.

Skills practiced

- Classes, constructors, properties, encapsulation, and domain validation.
- `Guid`, `DateTimeOffset`, `DateOnly`, enums, and nullable types.
- `List<T>`, `Dictionary<TKey,TValue>`, and `HashSet<T>`.
- LINQ `Where`, `Select`, `OrderBy`, `Count`, `Any`, and `GroupBy`.
- Exception handling and separation of business logic from console input.

### Module B: Task management

Each project contains tasks with priorities, statuses, deadlines, time estimates, tags, and optional dependencies.

Features

1. Create, edit, complete, reopen, and delete tasks.
2. Assign priorities: Low, Medium, High, or Critical.
3. Support statuses: Backlog, ToDo, InProgress, Blocked, and Completed.
4. Set due dates, estimated duration, and actual duration.
5. Assign multiple tags to tasks, such as `CSharp`, `LINQ`, `Interview`, and `DataStructures`.
6. Create parent tasks and subtasks.
7. Define dependencies between tasks, including dependencies across projects when permitted by the business rules.
8. Find overdue tasks, tasks due today, and tasks due within a configurable number of days.
9. Recommend the next task based on priority, deadline, status, and dependency completion.
10. Maintain task status history and activity records.
11. Search tasks by title, description, tag, project, priority, and status.
12. Identify tasks with missing deadlines, estimates, or other optional planning information.
13. Calculate the remaining estimated effort for a task or project.
14. Support bulk operations, such as changing the priority or status of several selected tasks.
15. Display the complete dependency chain for a task.

Important business rules

- A completed task cannot transition directly to InProgress; it must be explicitly reopened first.
- A task cannot be completed while its required dependencies remain incomplete.
- A task cannot depend on itself.
- Circular dependencies must be rejected.
- A completed task must have a completion timestamp.
- Reopening a task must follow a defined policy for its completion timestamp and history.
- Deleting a task must follow a defined policy for its subtasks and dependent tasks.
- A task's parent and dependency relationships must reference valid tasks.
- Project completion calculations must handle projects with zero tasks.
- Bulk operations must validate each requested change and report failures clearly.

Skills practiced

- Encapsulation through methods such as `Complete()`, `Reopen()`, and `AddDependency()`.
- Composition and object relationships.
- Interfaces for repositories, validation services, and task scheduling.
- `Queue<T>` for pending work and `Stack<T>` for undo/redo.
- LINQ filtering, sorting, joins, aggregation, and set operations.
- Graph traversal and cycle detection.
- Generic collections, date calculations, and domain-specific exceptions.

### Module C: Task search, filtering, and sorting

Build reusable query logic for projects and tasks. Do not create a separate search system for removed knowledge-base or note functionality.

Features

1. Search projects by name and description.
2. Search tasks by title and description using case-insensitive matching.
3. Filter tasks by project, tag, priority, status, due date, and completion state.
4. Combine filters using AND and OR conditions.
5. Sort tasks by deadline, priority, creation date, title, or estimated duration.
6. Support ascending and descending sorting.
7. Paginate results with configurable page size.
8. Save reusable task filters, such as "High-priority tasks due this week."
9. Display the total number of matching tasks before pagination.
10. Clear filters and restore the complete result set.
11. Find tasks matching any or all of a set of tags.
12. Find tasks that share one or more dependencies with a selected task.

Example commands

```
project list --status Active
project search "interview preparation"

task list --status InProgress --priority High
task list --tag LINQ --sort dueDate --order asc
task search "collection expressions"
task list --page 2 --page-size 10
task overdue
task due --within 7
task list --project <project-id>
task list --tag LINQ --tag CSharp --tag-mode all
```

These are illustrative command formats. Define and document the exact syntax you implement.

Skills practiced

- LINQ query composition and reusable `IEnumerable<T>` filtering abstractions.
- Deferred execution and materialization using `ToList()` and `ToArray()`.
- `StringComparison`, `StringSplitOptions`, and regular expressions.
- Generic methods and extension methods.
- Pagination using `Skip()` and `Take()`.
- `IComparer<T>` and `IComparable<T>`.
- Parsing command-line arguments and validating user input.

### Module D: Productivity analytics and reporting

Generate reports from projects, tasks, task histories, and time estimates.

Features

1. Count tasks by status and priority.
2. Calculate completion percentage for each project.
3. Group overdue tasks by project and priority.
4. Calculate average task completion time.
5. Count completed tasks per day, week, and month.
6. Rank projects by completion percentage.
7. Compare estimated and actual task durations.
8. Calculate total estimated remaining work.
9. Generate productivity summaries for a specified date range.
10. Export reports to CSV and JSON.
11. Calculate task completion rates by priority.
12. Identify projects with the highest number of blocked or overdue tasks.
13. Compare task completion trends across projects.
14. Display tasks completed within a specified period.
15. Calculate the distribution of tasks by tag.

Example report

```
PRODUCTIVITY SUMMARY
--------------------
Total projects:             8
Total tasks:              120
Completed:                 76
In progress:               18
Pending:                   16
Blocked:                   10
Overdue:                    7
Completion rate:         63.3%

PROJECT SUMMARY
---------------
Project: Learn C#
Total tasks:               25
Completed:                 15
Overdue:                    2
Estimated hours:           80
Actual hours:              52
Completion rate:         60.0%

TOP TASK TAGS
-------------
CSharp                     24
LINQ                       18
DataStructures             14
```

Skills practiced

- LINQ `GroupBy`, `Join`, `GroupJoin`, `Aggregate`, `Sum`, `Average`, `Min`, and `Max`.
- Anonymous types, tuples, and named result types.
- Nullable numeric values and empty-sequence handling.
- Date-range calculations and grouping by calendar period.
- `CultureInfo` and numeric formatting.
- CSV escaping and serialization.

### Module E: Persistence and backup

Persist projects, tasks, task dependencies, and supported activity history across application executions without a database.

Features

1. Save projects, tasks, tags, dependencies, and activity history to JSON.
2. Load the application state when the program starts.
3. Save changes after successful operations.
4. Create timestamped backups.
5. Restore a backup after validating its contents.
6. Detect missing, malformed, or incompatible data files.
7. Maintain a schema version for future format changes.
8. Export a complete application archive.
9. Recover from a corrupted primary file when a valid backup exists.
10. Prevent partial writes from corrupting the primary data file.
11. Validate entity IDs and relationships when loading data.
12. Support a reset operation with explicit confirmation.

Skills practiced

- `System.Text.Json`, `JsonSerializerOptions`, and serialization attributes.
- `System.IO`, streams, file paths, and asynchronous file operations.
- `FileStream`, `File.Move`, and temporary-file replacement strategies.
- `FileNotFoundException`, `JsonException`, and domain-specific exceptions.
- `CancellationToken` and `async`/`await`.
- DTOs, schema versioning, and mapping between domain objects and persisted data.
- Resource management and recovery from file errors.

For the initial implementation, use one JSON file for the complete application state. Later, compare it with separate files for projects, tasks, and activity history. Keep the design simple until persistence works reliably.

### Module F: Undo, history, and audit trail

Record important changes and support undo/redo for selected operations.

Features

1. Record project creation, updates, archival, and deletion.
2. Record task creation, updates, completion, reopening, and deletion.
3. Display the most recent activity events.
4. Undo the most recent supported operation.
5. Redo an operation that has been undone.
6. View the history of a particular task or project.
7. Record timestamps and relevant before-and-after values.
8. Prevent unsupported operations from corrupting history.
9. Define how undo/redo interacts with persistence.
10. Handle failed operations without leaving history and application state inconsistent.

Skills practiced

- `Stack<T>` for undo and redo.
- The Command pattern, using command objects with `Execute()` and `Undo()` methods.
- Interfaces, polymorphism, and encapsulation.
- Immutable event records and `record` types.
- Serialization of supported history events.
- Transaction-like operation boundaries and failure handling.

Start with undoing simple operations, such as renaming a project or changing a task's priority. Add support for task creation and deletion only after you have a reliable strategy for restoring relationships and dependencies.

### Module G: Import, export, and data validation

Make project and task data portable and handle external input reliably.

Features

1. Import projects and tasks from CSV.
2. Export projects and tasks to JSON and CSV.
3. Export individual project reports to plain text or Markdown.
4. Validate required columns and data types during import.
5. Report invalid rows with line numbers and error descriptions.
6. Provide a dry-run mode that validates data without changing application state.
7. Reject duplicate identifiers and invalid relationships.
8. Handle quoted CSV fields, embedded commas, and escaped quotes.
9. Display counts of imported, skipped, and rejected records.
10. Validate task references, including parent IDs and dependency IDs.
11. Allow users to choose how duplicate records are handled.
12. Support all-or-nothing import mode, where the data is committed only if validation succeeds.

Skills practiced

- Stream processing and resource disposal.
- Parsing and conversion with `TryParse()` methods.
- `Span<T>` and `ReadOnlySpan<char>` for optional advanced parsing.
- Custom exceptions and validation result objects.
- Staging data before committing it to the application state.
- Generic validation abstractions and error aggregation.

The .NET BCL does not provide a complete general-purpose CSV parser. For this exercise, implement a small parser that handles quoted fields correctly. This is a useful way to practice string processing, parsing, and edge-case handling.

## 3. Data structures you must implement and use

Use each data structure where it provides a meaningful benefit. Do not introduce structures solely to increase the number of classes in the project.

| Data structure                            | Where to use it                                                                      | What you should learn                     |
| ----------------------------------------- | ------------------------------------------------------------------------------------ | ----------------------------------------- |
| `List<T>`                                 | Project task lists and activity records                                              | Ordered collections, indexing, iteration  |
| `Dictionary<TKey,TValue>`                 | Lookup projects and tasks by ID                                                      | Hash-based lookup and key uniqueness      |
| `HashSet<T>`                              | Unique task tags and identifiers                                                     | Set membership and uniqueness             |
| `Queue<T>`                                | Processing pending import operations                                                 | FIFO processing                           |
| `Stack<T>`                                | Undo and redo operations                                                             | LIFO processing                           |
| `LinkedList<T>`                           | Optional task scheduling or history exercises where node insertion/removal is useful | Node-based insertion and removal          |
| `SortedDictionary<TKey,TValue>`           | Index tasks by due date                                                              | Ordered key-based access                  |
| `PriorityQueue<TElement,TPriority>`       | Scheduling eligible tasks                                                            | Priority-based retrieval                  |
| `ConcurrentDictionary<TKey,TValue>`       | Optional concurrent task processing                                                  | Thread-safe collection operations         |
| `ImmutableArray<T>` or `ImmutableList<T>` | Read-only snapshots of task or project data                                          | Immutable data and safe exposure of state |
| Adjacency list                            | Represent task dependencies                                                          | Graph representation and traversal        |

The optional `LinkedList<T>` exercise should remain an algorithmic exercise tied to task management, not a replacement for the removed notes feature. Do not introduce an LRU notes cache or a note-search index.

### Required data structure exercises

1. Task lookup index: Store tasks in a dictionary keyed by ID. Compare lookup performance with searching a list.
2. Tag index: Map each tag to a set of task IDs and support efficient retrieval of tasks sharing a tag.
3. Dependency graph: Represent task dependencies using an adjacency list and implement cycle detection with depth-first search.
4. Task scheduler: Use `PriorityQueue<TElement,TPriority>` to select the next eligible task, accounting for dependencies, deadlines, and priority.
5. Undo/redo manager: Use two stacks and define how new operations affect the redo history.
6. Optional linked-list exercise: Represent a task-processing sequence and investigate when linked-list operations are useful compared with list-based operations.
7. Immutable snapshots: Produce a read-only snapshot of project and task data for reports without exposing mutable internal collections.
8. Concurrent collection exercise: Compare a regular dictionary with `ConcurrentDictionary` in a controlled, optional concurrent-processing scenario.

### Complexity targets

| Operation                            | Expected complexity |
| ------------------------------------ | ------------------- |
| Dictionary lookup by key             | \\(O(1)\\) average  |
| Hash set membership check            | \\(O(1)\\) average  |
| List lookup by predicate             | \\(O(n)\\)          |
| List sorting                         | \\(O(n\log n)\\)    |
| Binary heap insertion/removal        | \\(O(\log n)\\)     |
| Graph traversal with adjacency lists | \\(O(V+E)\\)        |
| Cycle detection with DFS             | \\(O(V+E)\\)        |

These are typical expected bounds under appropriate assumptions. Measure actual performance using representative data before introducing optimizations.

## 4. Object-oriented design requirements

Use object-oriented design to enforce real project and task rules.

### Proposed domain model

```
public enum ProjectStatus
{
    Active,
    Completed,
    Archived
}

public enum TaskStatus
{
    Backlog,
    ToDo,
    InProgress,
    Blocked,
    Completed
}

public enum TaskPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}
```

The `Project` class should own its project-specific behavior and maintain its task relationships. The `TaskItem` class should own task state transitions, deadlines, estimates, and dependency rules.

For example, a task should expose a method such as `Complete()` rather than letting other classes assign `Status = TaskStatus.Completed` without validation.

### OOP concepts to demonstrate

Encapsulation

- Keep mutable collections private.
- Expose read-only views rather than publicly mutable lists.
- Change task state through domain methods that enforce invariants.
- Prevent invalid state transitions.
- Validate project names, task titles, IDs, and relationships.

Abstraction

Create interfaces for meaningful application capabilities, such as:

- `IProjectRepository`
- `ITaskRepository`
- `IFileStorage`
- `IClock`
- `IReportGenerator`
- `ITaskScheduler`

Polymorphism

Use polymorphism when implementations genuinely differ. For example:

- `IReportExporter`, implemented by CSV and JSON exporters.
- `INotificationService`, implemented by console and file-based notification services.
- `ICommand`, implemented by project-renaming, task-status-change, and task-creation commands.

Inheritance

Use inheritance sparingly. Do not make `Project` and `TaskItem` inherit from each other. Shared behavior should be represented through composition, interfaces, or a justified base abstraction.

Composition

- A project contains tasks.
- A task can contain subtasks.
- A task can depend on other tasks.
- A report service uses repositories and a clock.
- An import service uses parsers and validation services.

SOLID principles

- Single Responsibility: task validation, persistence, reporting, and console interaction have separate responsibilities.
- Open/Closed: add a new report export format without rewriting the entire reporting workflow.
- Liskov Substitution: implementations of an interface honor its behavioral contract.
- Interface Segregation: consumers depend only on the contracts they need.
- Dependency Inversion: application services depend on abstractions rather than concrete storage implementations.

## 5. Recommended application architecture

Use a small layered architecture and introduce it gradually.

Console / Presentation Layer

Menus, command parsing, prompts, output formatting

Application Layer

ProjectService, TaskService, SearchService, ReportService

Domain Layer

Project, TaskItem, enums, domain rules, value objects

Infrastructure Layer

JSON storage, CSV processing, backups, system clock

The diagram illustrates responsibilities rather than requiring each layer to depend directly on the one beneath it. In particular, the domain layer should not depend on console input or the file system.

### Suggested folder structure

```
PersonalManager/
├── src/
│   ├── PersonalManager.Console/
│   │   ├── Program.cs
│   │   ├── Commands/
│   │   └── Formatting/
│   ├── PersonalManager.Application/
│   │   ├── Services/
│   │   ├── Interfaces/
│   │   ├── DTOs/
│   │   └── Validation/
│   ├── PersonalManager.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── ValueObjects/
│   │   └── Exceptions/
│   └── PersonalManager.Infrastructure/
│       ├── Persistence/
│       ├── ImportExport/
│       └── Backup/
└── tests/
    ├── PersonalManager.Domain.Tests/
    ├── PersonalManager.Application.Tests/
    └── PersonalManager.Infrastructure.Tests/
```

Start with a single console project. Split it into multiple projects when the domain and application logic become sufficiently developed to benefit from separation.

## 6. LINQ mastery checklist

Use LINQ throughout project management, task search, dependency analysis, and reporting.

| LINQ feature                        | Example use case                                            |
| ----------------------------------- | ----------------------------------------------------------- |
| `Where`                             | Find overdue tasks                                          |
| `Select`                            | Convert tasks into report DTOs                              |
| `SelectMany`                        | Flatten tasks across projects                               |
| `OrderBy`, `ThenBy`                 | Sort by priority and deadline                               |
| `GroupBy`                           | Count tasks by status                                       |
| `Join`                              | Combine task and project data                               |
| `GroupJoin`                         | Show every project with its tasks, including empty projects |
| `Distinct`, `DistinctBy`            | Find unique tags or entities                                |
| `Union`, `Intersect`, `Except`      | Compare sets of task tags or task IDs                       |
| `Any`, `All`, `Contains`            | Check dependency and filtering conditions                   |
| `Count`, `Sum`, `Average`           | Calculate productivity metrics                              |
| `MinBy`, `MaxBy`                    | Find the earliest deadline or highest-priority task         |
| `Aggregate`                         | Calculate custom summaries                                  |
| `Skip`, `Take`                      | Implement pagination                                        |
| `Chunk`                             | Process imported tasks in batches                           |
| `ToDictionary`, `ToLookup`          | Build indexes for queries and reports                       |
| `Append`, `Prepend`, `Concat`       | Compose result sequences                                    |
| `First`, `FirstOrDefault`, `Single` | Retrieve entities with appropriate cardinality assumptions  |
| `GroupBy` with projection           | Produce grouped reports                                     |

### Required advanced LINQ exercises

1. Tasks due this week: Filter incomplete tasks within a date range and sort by deadline and priority.
2. Project completion dashboard: Calculate total, completed, and overdue tasks per project, including projects with zero tasks.
3. Tag analytics: Flatten task tags, normalize their casing, group them, and show the most frequently used tags.
4. Dependency analysis: Find tasks that depend on a selected task, directly or indirectly, and identify blocked tasks.
5. Efficient repeated queries: Compare repeatedly filtering a list with building and reusing a dictionary or lookup.
6. Deferred execution: Create a LINQ query, modify the source collection, and observe the result when enumerating the query. Compare it with calling `ToList()` immediately.
7. Empty sequences and nulls: Handle empty collections, nullable due dates, and missing duration values without misleading reports.
8. Query syntax versus method syntax: Write equivalent filtering, grouping, and joining operations in both styles.
9. LINQ versus loops: Implement the same report using LINQ and ordinary loops, then compare readability and performance.
10. Multiple enumeration: Identify repeated enumeration and refactor where it causes unnecessary computation or undesirable side effects.

For every exercise, explain why the query works, whether execution is deferred, whether it creates a new collection, and its approximate computational cost.

## 7. .NET standard library mastery checklist

The project should exercise the following areas through its existing project and task features.

| Area                          | Required exercises                                                                                  |
| ----------------------------- | --------------------------------------------------------------------------------------------------- |
| Strings                       | Normalize project names and task titles; compare strings; use `StringBuilder`                       |
| Dates and time                | Calculate deadlines, overdue status, time zones, and durations with `DateOnly` and `DateTimeOffset` |
| Collections                   | Use generic collections, enumerators, comparers, and read-only views                                |
| Files and directories         | Store application state, create backups, and export reports                                         |
| Streams                       | Read and write import/export files incrementally                                                    |
| Serialization                 | JSON options, DTO mapping, and schema versions                                                      |
| Exceptions                    | Handle validation errors, malformed data, and I/O failures                                          |
| Generics                      | Reusable validators, repository contracts, and result types                                         |
| Delegates and events          | Notify subscribers of task and project state changes                                                |
| Iterators                     | Use `yield return` for streaming filtered tasks                                                     |
| Async programming             | Perform asynchronous persistence and support cancellation                                           |
| Reflection                    | Optional discovery of command handlers or report exporters                                          |
| Regular expressions           | Validate command input or implement advanced task-title search                                      |
| Globalization                 | Culture-aware sorting and number/date formatting                                                    |
| Diagnostics                   | Use `Stopwatch`, logging, and performance measurements                                              |
| Random numbers                | Generate reproducible project and task data using a seeded generator                                |
| Environment and configuration | Read command-line arguments and configuration settings                                              |
| Resource management           | Use `IDisposable`, `using`, and proper stream lifecycle management                                  |
| Modern C#                     | Records, pattern matching, nullable reference types, and collection expressions                     |

The note-specific content hashing and note-search indexing exercises are removed. No replacement feature is required for those exercises.

For each remaining area, implement at least one relevant feature, write a test, and document why you selected that API.

## 8. Implementation roadmap

Implement the application in six stages. Each stage should leave the application in a working state.

## 01

Foundation: projects and tasks

Estimated effort: 4–6 hours

Implement project and task classes, enums, validation, CRUD operations, and a menu-driven interface. Store data in memory.

Focus: classes, properties, collections, exceptions, strings, and dates.

## 02

LINQ and task queries

Estimated effort: 5–8 hours

Implement filtering, sorting, pagination, task search, tag filtering, and productivity reports.

Focus: deferred execution, joins, aggregation, projections, and query composition.

## 03

OOP and architecture

Estimated effort: 5–8 hours

Separate domain logic from the console. Introduce application services, interfaces, dependency injection, and testable business rules.

Focus: encapsulation, abstraction, composition, polymorphism, and SOLID.

## 04

Data structures and algorithms

Estimated effort: 6–10 hours

Implement task indexes, undo/redo, dependency graphs, cycle detection, and priority-based scheduling.

Focus: hash-based collections, stacks, queues, heaps, graph traversal, and complexity analysis.

## 05

Persistence and file processing

Estimated effort: 5–8 hours

Add JSON persistence, CSV import/export, backups, error recovery, and asynchronous file operations.

Focus: streams, serialization, resource disposal, cancellation, and validation.

## 06

Testing, optimization, and polish

Estimated effort: 5–8 hours

Add unit tests, generate large datasets, profile task search and reporting, and improve error handling.

Focus: testability, complexity, allocations, maintainability, and performance.

Estimated total: 30–48 hours. This is a planning estimate. Adjust it according to your familiarity with the topics.

## 9. Non-functional requirements

- Maintainability: Keep business rules out of console menu handlers. Use cohesive classes and clearly named methods.
- Correctness: Reject invalid task transitions, duplicate identifiers, and circular dependencies.
- Reliability: Handle missing files, malformed JSON, invalid input, and interrupted operations gracefully.
- Testability: Run domain and application tests without requiring console input or real disk access.
- Performance: Avoid unnecessary repeated scans and materialization. Benchmark before optimizing.
- Usability: Provide helpful validation errors, consistent command syntax, and readable tabular output.
- Portability: Support Linux and Windows using a supported .NET SDK.
- Dependency discipline: Prefer BCL APIs and introduce third-party dependencies only when justified.

## 10. Testing requirements

Create a separate test project using a framework such as xUnit. The testing framework is a development dependency; the application itself should remain focused on the BCL.

| Test area          | Example                                                                   |
| ------------------ | ------------------------------------------------------------------------- |
| Project validation | Empty project names are rejected                                          |
| Task validation    | Empty titles and invalid priorities are rejected                          |
| State transitions  | Completed tasks require explicit reopening before returning to InProgress |
| Dependencies       | A task cannot be completed before its required dependencies               |
| Graph algorithms   | Direct and indirect dependency cycles are detected                        |
| LINQ reports       | Projects with no tasks do not cause division errors                       |
| Search             | Case-insensitive task search returns expected results                     |
| Pagination         | Empty pages and out-of-range page numbers are handled consistently        |
| Persistence        | Saving and reloading preserve project and task state                      |
| File errors        | Malformed JSON produces a useful recovery message                         |
| Undo/redo          | Undo restores the previous state and redo reapplies the operation         |
| CSV parsing        | Quoted commas and escaped quotes are handled correctly                    |
| Import validation  | Invalid task references are rejected before committing imported data      |
| Time handling      | Overdue calculations use a consistent clock and defined time-zone rules   |

Introduce an `IClock` abstraction when deterministic time-dependent tests are needed. A fake clock lets you test deadlines without relying on the actual date and time.

## 11. Optional advanced exercises

After completing the core project, choose two or three of these extensions.

1. Custom command parser: Support quoted arguments, optional parameters, aliases, and command validation.
2. Plugin-style exporters: Discover CSV and JSON exporter implementations using reflection.
3. Event-driven architecture: Publish domain events when tasks are completed or projects are archived.
4. Concurrent imports: Process independent import files concurrently while keeping shared state consistent.
5. Mini query language: Allow expressions such as `status = InProgress AND priority >= High`.
6. Performance benchmark: Generate 100,000 tasks and compare list scans, dictionary lookups, LINQ queries, and indexed search.
7. Custom generic result type: Represent successful operations and validation failures without using exceptions for ordinary invalid input.
8. Immutable domain model: Explore records, immutable collections, and replacing objects instead of mutating them.
9. Application configuration: Load settings from JSON and environment variables using BCL APIs.
10. Event sourcing exercise: Store supported domain events and reconstruct project or task state by replaying them.
11. Memory profiling: Investigate allocations from large LINQ pipelines, string processing, and collection growth.

These are optional extensions of project and task management, not replacements for the removed knowledge-base functionality.

For concurrent processing, remember that thread-safe collections do not automatically make multi-step business operations atomic.

## 12. Recommended learning resources

Use the official documentation to support your implementation.

- [C# documentation](https://learn.microsoft.com/en-us/dotnet/csharp/) — language fundamentals, types, OOP, and modern C#.
- [.NET fundamentals and standard libraries](https://learn.microsoft.com/en-us/dotnet/standard/) — collections, files, streams, and runtime APIs.
- [LINQ documentation](https://learn.microsoft.com/en-us/dotnet/csharp/linq/) — query operators and execution behavior.
- [Object-oriented programming in C#](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop) — classes, interfaces, inheritance, and polymorphism.
- [System.Text.Json overview](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview) — JSON serialization and deserialization.
- [.NET testing documentation](https://learn.microsoft.com/en-us/dotnet/core/testing/) — automated testing and test project setup.

## 13. Definition of done

The project is complete when you can demonstrate the following:

- Create, update, search, archive, and delete projects and tasks.
- Manage task statuses, priorities, deadlines, tags, subtasks, and dependencies.
- Explain why each major collection and data structure was selected.
- Write and explain LINQ queries involving filtering, projection, grouping, joins, aggregation, and set operations.
- Explain deferred execution, materialization, and multiple enumeration.
- Enforce business invariants through domain methods.
- Demonstrate interfaces, composition, polymorphism, and SOLID principles.
- Detect task dependency cycles and implement priority-based scheduling.
- Persist and restore application state using JSON.
- Import and export data while handling invalid input.
- Generate project and task productivity reports.
- Pass automated tests for domain rules, queries, persistence, and algorithms.
- Profile a performance bottleneck and justify any optimization.

Recommended starting point: Begin with project and task creation, editing, listing, and deletion in a single console project. Then add LINQ queries, refactor into a layered architecture, implement data structures and dependency algorithms, and finish with persistence and testing.

This keeps the project focused on your original learning goals without adding another functional area to compensate for the removed knowledge base.