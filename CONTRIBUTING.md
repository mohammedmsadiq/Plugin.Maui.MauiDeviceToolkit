# Contributing to Plugin.Maui.MauiDeviceToolkit

Thank you for your interest in contributing to Plugin.Maui.MauiDeviceToolkit! We welcome contributions from the community.

## How to Contribute

### Reporting Bugs

If you find a bug, please create an issue on GitHub with:
- A clear, descriptive title
- Steps to reproduce the issue
- Expected behavior
- Actual behavior
- Platform and version information (OS, .NET version, MAUI version)
- Code samples if applicable

### Suggesting Enhancements

We welcome feature requests! Please create an issue with:
- A clear description of the enhancement
- Use cases for the feature
- Any implementation ideas you might have

### Pull Requests

1. **Fork the repository** and create your branch from `main`
   ```bash
   git checkout -b feature/my-new-feature
   ```

2. **Make your changes**
   - Follow the existing code style
   - Add XML documentation comments for public APIs
   - Include unit tests for new functionality
   - Update README.md if needed

3. **Test your changes**
   ```bash
   dotnet build
   dotnet test
   ```

4. **Commit your changes**
   - Use clear, descriptive commit messages
   - Reference any related issues

5. **Push to your fork and submit a pull request**

## Development Setup

### Prerequisites
- .NET 8.0 SDK or higher
- Visual Studio 2022, VS Code, or JetBrains Rider
- Git

### Building the Project
```bash
# Clone your fork
git clone https://github.com/YOUR_USERNAME/Plugin.Maui.MauiDeviceToolkit.git
cd Plugin.Maui.MauiDeviceToolkit

# Restore packages
dotnet restore

# Build
dotnet build

# Run tests
dotnet test
```

## Code Style Guidelines

### General
- Use C# 12 features where appropriate
- Enable nullable reference types
- Follow Microsoft's C# coding conventions
- Use meaningful variable and method names

### Documentation
- Add XML documentation comments to all public APIs
- Include examples in comments for complex functionality
- Keep comments up-to-date with code changes

### Testing
- Write unit tests for new features
- Maintain or improve code coverage
- Use descriptive test names that explain what is being tested
- Follow the Arrange-Act-Assert pattern

## Project Structure

```
src/Plugin.Maui.MauiDeviceToolkit/
├── Interfaces/        # Public interface definitions
├── Services/          # Service implementations
└── MauiDeviceToolkit.cs  # Main API surface

tests/Plugin.Maui.MauiDeviceToolkit.Tests/
└── *Tests.cs          # Unit tests
```

## Adding New Features

When adding a new feature:

1. **Create an interface** in the `Interfaces/` folder
2. **Implement the service** in the `Services/` folder
3. **Add XML documentation** to all public members
4. **Write unit tests** in the test project
5. **Update README.md** with usage examples
6. **Update GETTING_STARTED.md** if it affects setup/usage

## Release Process

Releases are managed through GitHub Releases:

1. Update version number in `.csproj` file
2. Update CHANGELOG.md (if it exists)
3. Create a new release on GitHub with tag `vX.Y.Z`
4. GitHub Actions will automatically build and publish to NuGet

## Questions?

Feel free to open an issue for any questions about contributing.

## Code of Conduct

- Be respectful and inclusive
- Accept constructive criticism gracefully
- Focus on what is best for the community
- Show empathy towards other community members

Thank you for contributing! 🎉
