# Customer Management Application – Documentation

## 1. How long have you spent working on this exercise?

Approximately **3 hours**, including UI design, data validation, and implementing the save/load logic.

---

## 2. Choice of Frontend Technology

I chose **Avalonia UI**, a modern, cross-platform XAML-based UI framework that integrates well with the .NET ecosystem and supports MVVM architecture out of the box. It enables a responsive, maintainable UI and supports styling, animations, and modern design patterns.

---

## 3. Design Decisions Taken

- **MVVM Pattern**: Used the CommunityToolkit.Mvvm package to simplify property change notification and command management.
- **Dependency Injection Friendly**: Created interfaces for the save/load service (`ISaveLoadService<T>`) to allow for loose coupling and easy testing/mocking.
- **Validation Handling**: Implemented input validation logic directly within the `Customer` model and connected it to the UI using data binding and visibility triggers.
- **Save-on-Close**: Data is persisted in JSON format to a local application data folder when the application exits, ensuring no external database dependency while still retaining state.
- **UI Responsiveness**: Used Avalonia styling and layout controls to create a clean, animated, and responsive interface.
- **Modal Dialogs**: Added for Add/Edit operations with real-time validation and user feedback.

---

## 4. Any Issues Found

No significant issues were encountered during development. Avalonia provided a smooth and flexible experience for this type of application.

---

## 5. What Would Be Your Next Steps to Further Improve Your Work?

- **Async File IO**: Convert save/load operations to asynchronous to prevent any potential UI blocking during larger data saves.
- **Unit Tests**: Add unit tests for the validation logic and the save/load service.
- **More UI Polish**: Add animations for modal transitions and smooth error message appearance/disappearance.
- **Export/Import**: Allow users to export and import customer lists manually.
- **Sorting & Filtering**: Add support for basic customer filtering and sorting in the UI.

---
