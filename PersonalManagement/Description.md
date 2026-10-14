# .NET Core Practice Project: Personal Knowledge & Task Management System

The best way to refresh your .NET knowledge is to build a project that requires you to solve real problems using the standard library, LINQ, data structures, and object-oriented programming rather than merely implementing CRUD operations.

I recommend building a Personal Knowledge & Task Management System as a C# console application. It will manage projects, tasks, notes, tags, deadlines, activity history, and productivity reports. You will progressively implement features that require different parts of the .NET ecosystem.

The project should initially use an in-memory data store and the .NET standard library, without Entity Framework Core, external databases, or third-party libraries. This forces you to work directly with collections, algorithms, file I/O, serialization, exceptions, and object-oriented design.

## 1. Project overview

## Personal Knowledge & Task Management System

Application type: Console application | Language: C# | Framework: .NET 10 LTS or another supported .NET version

Build a command-line application for organizing personal projects, tracking tasks, maintaining a searchable knowledge base, and generating productivity analytics.

Project management

Projects, tasks, deadlines, priorities

Knowledge base

Notes, tags, categories, search

Data management

Collections, persistence, serialization

Analytics

LINQ reports, statistics, rankings

### Primary learning objectives

By the end of the project, you should be able to:

- Use the .NET Base Class Library (BCL) confidently, including collections, strings, dates, files, streams, serialization, and diagnostics.
- Write expressive LINQ queries using filtering, projection, sorting, grouping, joins, aggregation, set operations, and deferred execution.
- Select appropriate data structures based on access patterns, uniqueness, ordering, and performance.
- Apply encapsulation, abstraction, inheritance, polymorphism, interfaces, composition, and SOLID principles.
- Design a maintainable multi-layered console application with separation of concerns.
- Write unit tests and reason about algorithmic complexity.
- Persist application data safely without relying on an ORM or database.

## 2. Functional requirements and features

Implement the following modules. Each module is designed to exercise particular .NET skills, and later modules reuse concepts learned earlier.

### Module A: Project management

Users can create and manage multiple personal projects, such as learning C#, preparing for interviews, or building a side project.

Features

1. Create a project with a unique ID, name, description, creation date, and status.
2. Rename, update, archive, and delete projects.
3. List projects with pagination and configurable sorting.
4. Filter projects by status, creation date, and keyword.
5. Calculate project completion percentage based on completed tasks.
6. Display the number of pending, in-progress, overdue, and completed tasks per project.
7. Prevent deletion of a project when its dependencies or task-handling rules prohibit it.
8. Display project details with related tasks, notes, and recent activity.

Skills practiced

- Classes, constructors, properties, and encapsulation.
- `Guid`, `DateTimeOffset`, `DateOnly`, `Enum`, and nullable types.
- `List<T>`, `Dictionary<TKey,TValue>`, and `HashSet<T>`.
- LINQ's `Where`, `Select`, `OrderBy`, `Count`, `Any`, and `GroupBy`.
- Validation, exception handling, and separation of business rules from console input.

### Module B: Task management

Each project contains tasks with priorities, statuses, deadlines, and optional dependencies.

Features

1. Create, edit, complete, reopen, and delete tasks.
2. Assign each task a priority: Low, Medium, High, or Critical.
3. Support statuses: Backlog, ToDo, InProgress, Blocked, and Completed.
4. Set due dates, estimated duration, and actual duration.
5. Assign tags such as `CSharp`, `LINQ`, `Interview`, or `Research`.
6. Support parent tasks and subtasks.
7. Add dependencies, such as requiring task B to finish before task A can start.
8. Find overdue tasks, tasks due today, and tasks due within a configurable number of days.
9. Display the next recommended task based on priority, deadline, status, and dependency completion.
10. Maintain a history of status changes and task updates.

Important business rules

- A completed task cannot be marked as in progress without an explicit reopen operation.
- A task cannot be completed while its required dependencies remain incomplete.
- A task cannot depend on itself.
- Circular dependencies must be rejected.
- A completed task must have a completion timestamp.
- Deleting a task must follow a defined policy for its subtasks and dependent tasks.

Skills practiced

- Encapsulation through methods such as `Complete()`, `Reopen()`, and `AddDependency()`.
- Composition and object relationships.
- Interfaces for task repositories and validation services.
- `Queue<T>` for processing pending work and `Stack<T>` for undo history.
- LINQ filtering, sorting, joins, and aggregations.
- Graph traversal and cycle detection for dependency validation.

### Module C: Knowledge base and notes

The knowledge base stores technical notes and connects them to projects and tasks.

Features

1. Create notes with a title, body, category, creation timestamp, and modification timestamp.
2. Attach multiple tags to a note.
3. Link a note to one or more projects or tasks.
4. Search titles and note bodies using case-insensitive keyword matching.
5. Find notes that share tags or categories.
6. List recently modified and frequently accessed notes.
7. Find duplicate notes based on normalized content or a content hash.
8. Export selected notes to Markdown or plain text.
9. Support a pinned-notes feature.
10. Display related notes based on shared tags.

Skills practiced

- `string`, `StringBuilder`, string comparison, and text normalization.
- `Dictionary<string, List<Note>>` for a simple inverted search index.
- `HashSet<string>` for tag uniqueness and set operations.
- `File`, `Path`, `StreamReader`, and `StreamWriter`.
- `System.Security.Cryptography` for content hashes.
- LINQ `Contains`, `Intersect`, `Distinct`, `SelectMany`, and `GroupBy`.

### Module D: Search, filtering, and sorting

Build a reusable search system rather than implementing separate search logic for every screen.

Features

1. Search tasks by title, description, project, tag, priority, and status.
2. Combine filters using AND and OR conditions.
3. Sort by due date, priority, creation date, or title.
4. Support ascending and descending order.
5. Paginate results with configurable page size.
6. Search notes using multiple keywords.
7. Provide saved searches such as "High-priority tasks due this week."
8. Display the total number of matching records before pagination.
9. Allow users to clear filters and restore the full result set.

Example console commands

```
task list --status InProgress --priority High
task list --tag LINQ --sort dueDate --order asc
task search "collection expressions"
note search "deferred execution"
project list --status Active
task list --page 2 --page-size 10
```

Skills practiced

- LINQ query composition and reusable `IQueryable`-like or `IEnumerable<T>` filtering abstractions, without introducing a database.
- Deferred execution and materialization with `ToList()` and `ToArray()`.
- `StringComparison`, `StringSplitOptions`, and regular expressions.
- Generic methods and extension methods.
- Pagination with `Skip()` and `Take()`.
- Comparison logic with `IComparer<T>` and `IComparable<T>`.

### Module E: Productivity analytics and reporting

Generate useful statistics from the stored projects and tasks.

Features

1. Total tasks by status and priority.
2. Task completion rate per project.
3. Overdue tasks grouped by project and priority.
4. Average task completion time.
5. Tasks completed per day, week, and month.
6. Most frequently used tags.
7. Projects ranked by completion percentage.
8. Estimate-versus-actual duration comparisons.
9. A productivity summary for a specified date range.
10. Export reports to CSV and JSON.

Example report

```
PRODUCTIVITY SUMMARY
--------------------
Total tasks:             120
Completed:                76
In progress:              18
Pending:                  16
Blocked:                  10
Overdue:                   7
Completion rate:         63.3%

Top tags:
  CSharp                    24
  LINQ                      18
  DataStructures            14
```

Skills practiced

- LINQ `GroupBy`, `Join`, `GroupJoin`, `Aggregate`, `Sum`, `Average`, `Min`, and `Max`.
- Anonymous types, tuples, and named result types.
- Nullable numeric values and empty-sequence handling.
- Date range calculations and grouping by calendar period.
- `CultureInfo` and numeric formatting.
- CSV escaping and serialization.

### Module F: Persistence and backup

Persist the application's state across executions without a database.

Features

1. Save projects, tasks, notes, tags, and activity history to JSON files.
2. Load data when the application starts.
3. Automatically save changes after successful operations.
4. Create timestamped backups.
5. Restore a backup after validating its contents.
6. Detect missing, malformed, or incompatible data files.
7. Maintain a schema version to support future data format changes.
8. Export a complete application archive.
9. Recover from a corrupted primary file when a valid backup exists.
10. Prevent partial writes from leaving the primary data file corrupted.

Skills practiced

- `System.Text.Json`, `JsonSerializerOptions`, and serialization attributes.
- `System.IO`, streams, file paths, and asynchronous file operations.
- `FileStream`, `File.Move`, and temporary-file replacement strategies.
- `FileNotFoundException`, `JsonException`, and domain-specific exceptions.
- `CancellationToken` and `async`/`await`.
- Data transfer objects (DTOs), versioning, and mapping between domain objects and persisted data.

For the initial version, use one JSON file per aggregate or a single application-state file. Later, compare the trade-offs of both approaches.

### Module G: Undo, history, and audit trail

Make the application capable of explaining and reversing certain changes.

Features

1. Record task creation, updates, completion, and deletion.
2. Display the latest activity events.
3. Undo the most recent supported operation.
4. Redo an operation that has been undone.
5. View the history of a particular task.
6. Record timestamps and relevant before-and-after values.
7. Prevent unsupported operations from corrupting history.

Skills practiced

- `Stack<T>` for undo and redo.
- The Command pattern, using command objects with `Execute()` and `Undo()` methods.
- Interfaces, polymorphism, and encapsulation.
- Immutable event records and `record` types.
- Serialization of supported history events.
- Designing explicit operation boundaries and handling failures safely.

Start with undoing simple operations such as renaming a project or changing a task's status. Avoid attempting arbitrary undo of every operation until the basic model is stable.

### Module H: Import, export, and data validation

Make data portable and test your handling of external input.

Features

1. Import tasks and notes from CSV.
2. Export selected entities to JSON, CSV, and Markdown.
3. Validate required columns and data types during import.
4. Report invalid rows with line numbers and error descriptions.
5. Support a dry-run mode that validates data without modifying the application.
6. Reject duplicate identifiers and invalid relationships.
7. Handle quoted CSV fields, embedded commas, and escaped quotes.
8. Provide a summary of imported, skipped, and rejected records.

Skills practiced

- Stream processing and resource disposal.
- Parsing and conversion with `TryParse()` methods.
- `Span<T>` and `ReadOnlySpan<char>` for optional advanced parsing exercises.
- Custom exceptions and validation result objects.
- Transactions at the application level, using staging data and committing only after validation.
- Generic validation abstractions and error aggregation.

Note that the .NET BCL does not provide a complete general-purpose CSV parser. For this exercise, implement a small parser that correctly handles quoted fields, or use a third-party library only after completing the BCL-focused version.

## 3. Data structures you must implement and use

Do not use every data structure simply to demonstrate it. Give each structure a specific responsibility and document why it is appropriate.

| Data structure                            | Where to use it                                | What you should learn                     |
| ----------------------------------------- | ---------------------------------------------- | ----------------------------------------- |
| `List<T>`                                 | Tasks, notes, activity records                 | Ordered collections, indexing, iteration  |
| `Dictionary<TKey,TValue>`                 | Lookup by project or task ID                   | Hash-based lookup and key uniqueness      |
| `HashSet<T>`                              | Unique tags and IDs                            | Set membership and uniqueness             |
| `Queue<T>`                                | Background work queue or import processing     | FIFO processing                           |
| `Stack<T>`                                | Undo and redo operations                       | LIFO processing                           |
| `LinkedList<T>`                           | Recently viewed items or an optional LRU cache | Node-based insertion and removal          |
| `SortedDictionary<TKey,TValue>`           | Tasks indexed by due date                      | Ordered key-based access                  |
| `PriorityQueue<TElement,TPriority>`       | Scheduling the next eligible task              | Priority-based retrieval                  |
| `ConcurrentDictionary<TKey,TValue>`       | Optional concurrent processing                 | Thread-safe collection operations         |
| `ImmutableArray<T>` or `ImmutableList<T>` | Read-only snapshots                            | Immutable data and safe exposure of state |
| `Dictionary<string, HashSet<Guid>>`       | Inverted index for note search                 | Efficient search by term                  |
| Adjacency list                            | Task dependencies                              | Graph representation and traversal        |

### Required data structure exercises

Implement these as separate exercises or services within the application:

1. Task lookup index: Store tasks in a dictionary keyed by ID. Compare its lookup performance with searching a list.
2. Tag index: Map each tag to a set of task IDs. Support efficient retrieval of tasks sharing a tag.
3. Dependency graph: Represent dependencies as an adjacency list and implement cycle detection using depth-first search.
4. Task scheduler: Use `PriorityQueue<TElement, TPriority>` to select the next eligible task. Account for dependencies, deadlines, and priorities.
5. Undo/redo manager: Use two stacks and define how a new operation affects the redo history.
6. Recent notes: Implement an optional least-recently-used (LRU) cache using a dictionary and a linked list.
7. Search index: Map normalized words to sets of note IDs, then combine matching sets for multi-keyword queries.

### Complexity targets

You should be able to explain the expected complexity of your implementation, not just make it work.

| Operation                            | Expected complexity |
| ------------------------------------ | ------------------- |
| Dictionary lookup by key             | \\(O(1)\\) average  |
| Hash set membership check            | \\(O(1)\\) average  |
| List lookup by predicate             | \\(O(n)\\)          |
| List sorting                         | \\(O(n\log n)\\)    |
| Binary heap insertion/removal        | \\(O(\log n)\\)     |
| Graph traversal with adjacency lists | \\(O(V+E)\\)        |
| Cycle detection with DFS             | \\(O(V+E)\\)        |

These are typical expected bounds under appropriate assumptions, not guarantees for every workload. Measure actual performance with representative data before making optimization decisions.

## 4. Object-oriented design requirements

The application should demonstrate OOP through real domain rules rather than artificial inheritance hierarchies.

### Proposed domain model

```

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

public sealed class Project
{
    private readonly List<TaskItem> _tasks = new();

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

    public Project(string name)
    {
        Rename(name);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name is required.");

        Name = name.Trim();
    }

    public void AddTask(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);

        if (_tasks.Any(t => t.Id == task.Id))
            throw new InvalidOperationException(
                "Task already exists in this project.");

        _tasks.Add(task);
    }
}

```

This is an illustrative starting point. You will need to initialize non-nullable properties appropriately and expand the model as the project develops.

### OOP concepts to demonstrate

Encapsulation

- Keep mutable collections private.
- Expose read-only views instead of publicly mutable lists.
- Change state through domain methods that enforce invariants.
- Do not let callers mark a task complete by directly assigning a property.

Abstraction

Define interfaces around application capabilities, such as:

- `IProjectRepository`
- `ITaskRepository`
- `INoteRepository`
- `IFileStorage`
- `IClock`
- `IReportGenerator`

These interfaces should represent meaningful contracts, not exist merely to wrap every method.

Polymorphism

Use polymorphism when implementations genuinely differ. For example:

- `IReportExporter`, implemented by CSV and JSON exporters.
- `INotificationService`, implemented by console and file-based notification services.
- `ICommand`, implemented by rename, status-change, and task-creation commands.

Inheritance

Use inheritance sparingly. A shared base entity or common abstraction may be justified, but `Project`, `TaskItem`, and `Note` should not inherit from one another simply because they are domain entities.

Composition

- A project contains tasks.
- A task references its dependencies.
- A task or project can have associated tags.
- A report service uses repositories and a clock.
- An import service uses parsers and validation services.

SOLID principles

- Single Responsibility: task validation, persistence, reporting, and user interaction have distinct responsibilities.
- Open/Closed: add a new export format without rewriting the entire reporting workflow.
- Liskov Substitution: implementations of an interface honor its behavioral contract.
- Interface Segregation: consumers depend on the small contracts they need.
- Dependency Inversion: application services depend on abstractions rather than concrete storage classes.

## 5. Recommended application architecture

Use a small layered architecture. Avoid introducing a full enterprise architecture before the core functionality works.

Console / Presentation Layer

Menus, command parsing, prompts, formatting

Application Layer

ProjectService, TaskService, SearchService, ReportService

Domain Layer

Entities, enums, domain rules, value objects

Infrastructure Layer

JSON storage, CSV parsing, backup, system clock

The diagram shows conceptual responsibilities, not a requirement that every layer directly depend on the layer beneath it. In particular, the domain layer should not depend on the console or file system.

A practical folder structure:

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
│       ├── Search/
│       └── Backup/
└── tests/
    ├── PersonalManager.Domain.Tests/
    ├── PersonalManager.Application.Tests/
    └── PersonalManager.Infrastructure.Tests/
```

For the first milestone, you can put everything into a single console project. Split it into projects when you introduce application services and persistence. This avoids spending your initial learning time on project wiring instead of C#.

## 6. LINQ mastery checklist

LINQ is one of the central goals of this project. Each operation below should appear in a meaningful feature and be accompanied by a small test.

| LINQ feature                        | Example use case                                                      |
| ----------------------------------- | --------------------------------------------------------------------- |
| `Where`                             | Find overdue tasks                                                    |
| `Select`                            | Convert tasks into summary DTOs                                       |
| `SelectMany`                        | Flatten tasks from multiple projects                                  |
| `OrderBy`, `ThenBy`                 | Sort by priority and due date                                         |
| `GroupBy`                           | Count tasks by status                                                 |
| `Join`                              | Combine task and project data                                         |
| `GroupJoin`                         | Show every project with its tasks, including empty projects           |
| `Distinct`, `DistinctBy`            | Find unique tags or entities                                          |
| `Union`, `Intersect`, `Except`      | Compare tag sets and saved searches                                   |
| `Any`, `All`, `Contains`            | Check dependency and filtering conditions                             |
| `Count`, `Sum`, `Average`           | Calculate productivity metrics                                        |
| `MinBy`, `MaxBy`                    | Find the earliest deadline or highest-priority task                   |
| `Aggregate`                         | Build a custom summary                                                |
| `Skip`, `Take`                      | Implement pagination                                                  |
| `Chunk`                             | Process imports in batches                                            |
| `ToDictionary`, `ToLookup`          | Build indexes for joins and searches                                  |
| `Append`, `Prepend`, `Concat`       | Compose result sequences                                              |
| `First`, `FirstOrDefault`, `Single` | Retrieve individual entities with the correct cardinality assumptions |
| `GroupBy` with result projection    | Produce grouped reports in a single query                             |

### Required advanced LINQ exercises

Exercise 1: Tasks due this week

Filter incomplete tasks with a due date within a defined date range, then order them by deadline and priority.

Exercise 2: Project completion dashboard

For each project, calculate total tasks, completed tasks, overdue tasks, and completion percentage. Include projects with zero tasks.

Exercise 3: Tag analytics

Flatten all task tags, normalize their casing, group them, and display the most frequently used tags.

Exercise 4: Related notes

Find notes that share at least two tags with a given note. Exclude the source note and order results by the number of shared tags.

Exercise 5: Efficient repeated queries

Compare repeatedly filtering a list with building a dictionary or lookup once and reusing it.

Exercise 6: Deferred execution

Create a LINQ query, modify the source collection, and observe what happens when you enumerate the query. Compare this with calling `ToList()` immediately.

Exercise 7: Empty sequences and nulls

Handle empty collections, nullable due dates, and missing duration values without unexpected exceptions or misleading statistics.

Exercise 8: Query syntax versus method syntax

Write a query involving filtering, grouping, and joins in both styles. Explain which style is clearer for each case.

Exercise 9: LINQ versus loops

Implement the same report using LINQ and ordinary loops. Compare readability, intermediate allocations, and performance with realistic data.

Exercise 10: Multiple enumeration

Identify code that enumerates the same sequence more than once. Refactor it when repeated computation or side effects make that undesirable.

A key requirement is to explain why each query works, whether execution is deferred, whether it creates a new collection, and what its approximate computational cost is.

## 7. .NET standard library mastery checklist

The BCL is much broader than collections and LINQ. The project should deliberately exercise these areas.

| Area                          | Required exercises                                                          |
| ----------------------------- | --------------------------------------------------------------------------- |
| Strings                       | Normalization, comparison, interpolation, `StringBuilder`                   |
| Dates and time                | Due-date calculations, time zones, `DateOnly`, `DateTimeOffset`             |
| Collections                   | Generic collections, enumerators, comparers, read-only views                |
| Files and directories         | Storage, backups, export paths, directory creation                          |
| Streams                       | Read and write files incrementally                                          |
| Serialization                 | JSON options, DTO mapping, schema versions                                  |
| Exceptions                    | Validation errors, malformed data, I/O failures                             |
| Generics                      | Reusable validators, repository contracts, result types                     |
| Delegates and events          | Activity notifications and state-change events                              |
| Iterators                     | `yield return` for streaming filtered results                               |
| Async programming             | Asynchronous persistence and cancellation                                   |
| Reflection                    | Optional discovery of command handlers or exporters                         |
| Regular expressions           | Advanced search and input validation                                        |
| Globalization                 | Culture-aware sorting and number/date formatting                            |
| Cryptography                  | Hash note content for duplicate detection                                   |
| Diagnostics                   | `Stopwatch`, logging, and performance measurements                          |
| Random numbers                | Generate reproducible demo data with a seeded generator                     |
| Environment and configuration | Read command-line arguments and configuration settings                      |
| Resource management           | `IDisposable`, `using`, and stream lifecycle management                     |
| Modern C#                     | Records, pattern matching, nullable reference types, collection expressions |

For each area, implement at least one feature, write a test, and record the reason for choosing that API.

## 8. Implementation roadmap

Build the project in stages. Each stage should leave you with a working application.

## 01

Foundation: domain model and console menus

Estimated effort: 4–6 hours

Create projects, tasks, enums, validation methods, and a menu-driven interface. Store everything in memory.

Focus: classes, properties, collections, exceptions, strings, and dates.

## 02

LINQ and search

Estimated effort: 5–8 hours

Implement filtering, sorting, pagination, task search, tag search, and grouped reports.

Focus: deferred execution, joins, aggregation, projections, and query composition.

## 03

OOP and architecture

Estimated effort: 5–8 hours

Separate domain logic from the console. Introduce application services, interfaces, dependency injection, and testable business rules.

Focus: encapsulation, abstraction, composition, polymorphism, and SOLID.

## 04

Data structures and algorithms

Estimated effort: 6–10 hours

Implement task indexes, undo/redo, a dependency graph, cycle detection, and priority-based scheduling.

Focus: hash-based collections, stacks, queues, heaps, graph traversal, and complexity analysis.

## 05

Persistence and file processing

Estimated effort: 5–8 hours

Add JSON persistence, CSV import/export, backups, error recovery, and asynchronous file operations.

Focus: streams, serialization, resource disposal, cancellation, and data validation.

## 06

Testing, optimization, and polish

Estimated effort: 5–8 hours

Add unit tests, generate large datasets, profile search and reporting, and improve error handling.

Focus: testability, complexity, allocations, maintainability, and performance.

Estimated total: 30–48 hours. This is a planning estimate, not a fixed deadline. Spend additional time on any area where you cannot explain the underlying behavior confidently.

## 9. Non-functional requirements

Treat these as engineering requirements rather than optional polish.

- Maintainability: No business rules inside menu handlers. Use named methods and cohesive classes.
- Correctness: Invalid task transitions, duplicate IDs, and circular dependencies must be rejected.
- Reliability: Handle missing files, malformed JSON, invalid user input, and interrupted operations gracefully.
- Testability: Core domain and application logic must run in tests without requiring console input or actual disk access.
- Performance: Avoid unnecessary repeated scans and materialization. Benchmark before introducing complex optimizations.
- Usability: Provide helpful validation errors, consistent command syntax, and readable tabular output.
- Portability: The project should run on Linux and Windows using the supported .NET SDK.
- No unnecessary dependencies: Prefer BCL APIs. Add third-party packages only when a feature provides a good reason.

## 10. Testing requirements

Create a separate test project using a unit-testing framework such as xUnit. The framework is an intentional development dependency; the application itself should remain BCL-focused.

At a minimum, write tests for the following cases:

| Test area         | Example                                                                           |
| ----------------- | --------------------------------------------------------------------------------- |
| Domain validation | Empty project names are rejected                                                  |
| State transitions | Completed tasks cannot be completed twice without a defined policy                |
| Dependencies      | A task cannot be completed before its required dependencies                       |
| Graph algorithms  | Cycles are detected, including indirect cycles                                    |
| LINQ reports      | Empty projects have a completion rate of zero rather than causing division errors |
| Search            | Case-insensitive matching returns expected results                                |
| Pagination        | Empty pages and out-of-range page numbers are handled consistently                |
| Persistence       | Saving and reloading preserve supported application state                         |
| File errors       | Malformed JSON produces a useful recovery message                                 |
| Undo/redo         | Undo restores the previous state and redo reapplies the operation                 |
| CSV parsing       | Quoted commas and escaped quotes are parsed correctly                             |
| Time handling     | Overdue calculations use a defined time zone and consistent clock                 |

Introduce an `IClock` abstraction if you need deterministic tests for current-time-dependent logic. A fake clock lets you test deadlines without depending on when the test runs.

## 11. Stretch goals for advanced practice

After the core project is complete, choose two or three of these extensions.

1. Custom command parser: Support quoted arguments, optional parameters, aliases, and command validation.
2. Plugin-style exporters: Discover implementations of an export interface using reflection.
3. Event-driven architecture: Publish domain events when tasks are completed or projects are archived.
4. Concurrent imports: Process independent files concurrently while keeping shared state consistent.
5. LRU caching: Cache expensive search results and invalidate them after relevant updates.
6. Mini query language: Allow expressions such as `status = InProgress AND priority >= High`.
7. Performance benchmark: Generate 100,000 tasks and compare list scans, dictionary lookups, LINQ queries, and indexed search.
8. Custom generic result type: Represent success and validation failure without using exceptions for ordinary invalid input.
9. Immutable domain model: Explore records, immutable collections, and replacing objects instead of mutating them.
10. Application configuration: Load settings from JSON and environment variables using BCL APIs.
11. Custom event sourcing exercise: Store supported domain events and reconstruct a task's state by replaying them.
12. Memory profiling: Investigate allocations from large LINQ pipelines, string processing, and collection growth.

For concurrent processing, distinguish between thread-safe collections and thread-safe application behavior. A `ConcurrentDictionary` does not automatically make a multi-step business operation atomic.

## 12. Recommended learning resources

Use official documentation alongside implementation. These resources are free to access.

[LINQ: Foundational Concepts](https://images.openai.com/static-rsc-4/h5YNEMKU21OhnWmK00cU3iY6uwWAb3USMiy3EPNpJTJu3MZed1vX7P4J_NjGTlApAiFBCElQNtHkLSFDIEBNKrwwflzy_uBmOhdQu_SIFInn36iq8vWoG5mQpSWipVyjh5jfjg7sfTozoRZhcxNHZzlsHEOqxSncZnwD1m7Yykc?purpose=inline)

C# and .NET documentation

Learn language features, the runtime, collections, generics, and the BCL.

[C# documentation](https://learn.microsoft.com/en-us/dotnet/csharp/)

[.NET fundamentals and standard libraries](https://learn.microsoft.com/en-us/dotnet/standard/)

[Building LINQ from scratch with Stephen Toub > Programming > earthli News](https://images.openai.com/static-rsc-4/7ow_o0393TKJgsiMgg5X38h-UzCvp_uIiBdy-fuKhjq08X6oBxoWuBVMpFu_-sG4GLG1Ks1fHHbBAL5FUKnbZIeE1jRYOEnv3SalGUVAMNMOfZx2TDcRRo1_frX5_4gHVfIL_B_pcbvrioCTq5RFdIDuUWquj3XSEWnfOWHyk_Q?purpose=inline)

LINQ

Review query operators, execution behavior, and transformations.

[Language Integrated Query (LINQ)](https://learn.microsoft.com/en-us/dotnet/csharp/linq/)

[C# for Beginners | Microsoft Learn](https://images.openai.com/static-rsc-4/pDAgOckRK9b_2FE0xYuL8JFnLcw9orat-kzWQU5I-Tr340aSSTNIfPrt8a2jFWCdjvi4M4Ow6aQ4jFLKPgxb2D-ulIs4Hqh7Z-VEW_byc5Dkdndzl-S2Tv9KVX-kJtWGVRqOe3JfzotZJA5DjwadOGCx12UfevkB_0eLQTzZYLU?purpose=inline)

Object-oriented programming

Study classes, interfaces, inheritance, polymorphism, and abstraction.

[Object-oriented programming in C#](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop)

[Welcome to System.Text.Json - How to read & write .NET objects as JSON | Microsoft Learn](https://images.openai.com/static-rsc-4/pcFDjYnlUBYv79Oxl6NcFGh1ggzsuqDhXcT2vZZ4OBGfxFwVf18lLyxfadKOPlmDArA7wc3QIMXzf8CZcaoW9z-3CA3qVT5kknfDhPGzEvt9KkqfilYDJd_1tTP26WsD2Qw_EFMAr6M9ZkL4w2-wY5_DKI1TmNNeOzZ2M1z9byk?purpose=inline)

JSON and file persistence

Learn how to serialize and deserialize application state using the built-in JSON APIs.

[System.Text.Json overview](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview)

[Assertion techniques to create Unit Tests in C#](https://images.openai.com/static-rsc-4/866Q4kajnxtHewBGY5W95wzGQovxf6SQTpwcfyqq-q3xtdOXJwhaGB36G88muPLRGi7yx_j5jaBUUfdvq6-Jt3k4WHmKCva8gvXMswn4FPKixzQydPDS5uNj_x7YXJGc3d7JbFRav9-ZjwhaWON7I3R4BSbSYfuJu8oTk0CWLxg?purpose=inline)

Unit testing

Learn how to test application behavior and isolate dependencies.

[.NET testing documentation](https://learn.microsoft.com/en-us/dotnet/core/testing/)

## 13. Definition of done

Consider the project complete when you can demonstrate all of the following:

- Create, update, search, and delete projects, tasks, and notes.
- Explain why each major collection or data structure was selected.
- Write and explain LINQ queries involving filtering, projection, grouping, joins, aggregation, and set operations.
- Explain deferred execution, materialization, and multiple enumeration.
- Enforce business invariants through domain methods.
- Demonstrate interfaces, composition, and polymorphism in meaningful use cases.
- Detect task dependency cycles and implement priority-based scheduling.
- Persist and restore application state using JSON.
- Import and export data while handling invalid input.
- Generate useful reports and explain their computational costs.
- Pass automated tests for domain rules, queries, persistence, and algorithms.
- Profile a performance bottleneck and justify any optimization.

My recommendation: Start with Modules A and B in a single console project. Do not begin with dependency injection, repositories, or elaborate architecture. First build a correct domain model and make the application work. Then introduce LINQ, refactor the architecture, implement the data structures, and add persistence in that order. This approach will give you a much stronger refresher than writing a large application all at once.