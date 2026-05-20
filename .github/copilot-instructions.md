# Copilot Custom Instructions

## Commit Messages

Use [Conventional Commits](https://www.conventionalcommits.org/) format: `<type>(<scope>): <description>`

### Type (required)
What **kind** of change:
- `feat`: new feature
- `fix`: bug fix
- `refactor`: code restructuring without behavior change
- `test`: adding or updating tests
- `docs`: documentation changes
- `chore`: maintenance tasks (dependencies, tooling)
- `ci`: CI/CD pipeline changes
- `perf`: performance improvements

### Scope (optional)
What **component/area** of the codebase:
- Use the module, feature, or folder name (e.g., `calc`, `operations`, `ui`)
- For test commits: use the component being tested, NOT `test` or `tests`
  - ✅ `test(calc): ...` - testing calculator logic
  - ❌ `test(tests): ...` - redundant
  - ✅ `test: ...` - omit scope for broad test changes
- Omit if the change applies broadly across the project
- **Never repeat the type as the scope** (e.g., `test(test)`, `docs(docs)`, `ci(ci)`)

### Description (required)
- Use imperative mood, lowercase: "add feature" not "added feature" or "adds feature"
- Be specific and descriptive (minimum 3-4 words)
- Keep under 72 characters
- Do not end with a period

### Additional Rules
- Reference GitHub issues in the footer when applicable (e.g., `Closes #42`)
- Include `BREAKING CHANGE:` in the footer for breaking changes

### Examples

```
feat(calc): add modulo operation support
fix(calc): handle division by zero correctly
refactor(operations): extract validation into separate method
test(calc): add edge cases for negative numbers
test(calc): add xUnit project with full operator coverage
test: verify error handling across all operations
docs: update README with usage examples
chore: update NuGet package dependencies
ci: add code coverage reporting
```