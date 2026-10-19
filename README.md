# HTML Serializer & Query

A **C# (.NET 8)** tool that downloads a web page, parses its HTML into an **element tree**, and queries the tree with **CSS-like selectors** – similar to a small version of `document.querySelectorAll`.

## Features

- Load the HTML of any URL with `HttpClient`
- Parse the HTML into a tree of `HtmlElement` objects with id, classes, attributes, inner HTML, parent and children
- Recognize HTML tags and self-closing (void) tags from JSON definition files
- Query the tree with selectors that combine tag name, `#id` and `.class`, including descendant selectors (for example `div .card p`)
- Traverse the tree with `Descendants()` and `Ancestors()`

## How It Works

1. **Serialize** – the HTML is split into tags and text with regular expressions, and a tree is built while tracking open and closing tags.
2. **Parse the selector** – a selector string is converted into a chain of `Selector` objects (tag, id, classes, child).
3. **Query** – the tree is searched recursively, and the matches are collected in a `HashSet` to avoid duplicates.

## Project Structure

```
Html Serializer/Html Serializer/
├── Program.cs       # Loads a page and runs a sample query
├── HtmlElement.cs   # Tree node, tree building, traversal and selector matching
├── Selector.cs      # Parses a selector string into a selector chain
└── HtmlHelper.cs    # Loads the tag lists (singleton)
JSON Files/          # HTML tags and void tags
```

## Getting Started

```bash
cd "Html Serializer"
dotnet run --project "Html Serializer"
```

Change the URL and the selector in `Program.cs` to query any page.
