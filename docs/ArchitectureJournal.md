
# BlazorExpo Architecture Journal

## Foundation Initiative (Version 2.0)

**Date:** July 2, 2026  
**Status:** Active

---

## Vision

BlazorExpo is a modern Blazor portfolio application designed to demonstrate:

- Enterprise-style architecture using Blazor
- Feature-based organization
- Centralized application state management
- Reusable UI components
- Clean separation of concerns
- Practical integration patterns for real-world applications

The goal is not just to build features, but to build a **maintainable system that can scale with new features easily**.

---

## Core Architectural Direction

We are transitioning the application into a **state-driven, feature-oriented architecture**.

Key principle:

> All shared UI state flows through a single source of truth: `AppState`.

---

## Current Architecture (Baseline)

### Structure

---

### State Management

- `AppState` is the single source of truth for:
  - Theme
  - Footer branding
  - Future UI state (navigation, layout, etc.)

- Legacy services (e.g., ThemeService, FooterBrandService) are being removed.

---

### Branding System

- Footer branding is centralized via:
  - `FooterBrands` (static catalog)
  - `FooterBrand` (model)
  - `AppState.CurrentFooterBrand`

- Feature pages are responsible for selecting appropriate branding.

---

### Feature Ownership

Features are beginning to define their own UI context.

Example:
- MovieTime feature sets MovieDB branding
- Themes feature controls UI theme selection

---

## Current Constraints

- No feature-specific service should own shared UI state
- No duplicate sources of truth for theme or branding
- UI state changes must flow through AppState

---

## Goals of Foundation 2.0

1. Eliminate all duplicate UI state services
2. Ensure AppState is the only UI state container
3. Establish consistent startup initialization
4. Prevent “refresh fixes state” behavior
5. Prepare for feature-based expansion

---

## What “Good” Looks Like

A new feature should be able to:

- Define its own branding via `FooterBrands`
- Set its UI context via `AppState`
- Plug into existing layout without modifying infrastructure
- Avoid introducing new global services

---

## Guiding Principle

> “Features own behavior. AppState owns state. Components render UI.”

---

## Status Summary

- ✔ Folder structure established
- 🟡 AppState migration in progress
- 🟡 Legacy services being removed
- ⚪ Startup initialization not finalized
- ⚪ Local storage persistence not implemented
- ⚪ Feature context abstraction not yet introduced

---

## Next Milestone

Complete AppState consolidation:

- Remove remaining legacy services
- Centralize UI state flow
- Stabilize startup initialization

This will complete the Foundation phase of BlazorExpo.