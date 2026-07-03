# BlazorExpo Decision Log (ADR)

## Foundation 2.0 – Initial Architectural Decisions

---

## ADR-001: Introduce AppState as Single Source of Truth

**Date:** July 2, 2026

### Decision

We introduced `AppState` as the central container for all shared UI state.

### Context

The application originally used multiple services:
- ThemeService
- FooterBrandService
- JS-based theme state

This created multiple competing sources of truth.

### Decision

Consolidate all UI state into a single service:

- AppState manages theme
- AppState manages footer branding
- AppState will manage future UI state

### Consequences

**Positive:**
- Single source of truth
- Predictable UI updates
- Easier debugging
- Simplified architecture

**Tradeoffs:**
- Slightly larger central service
- Requires careful discipline to avoid bloating AppState

---

## ADR-002: Introduce FooterBrand Catalog Pattern

**Date:** July 2, 2026

### Decision

Move all footer branding definitions into a centralized static catalog (`FooterBrands`).

### Context

Footer branding was previously defined inline across multiple pages.

### Decision

- Create `FooterBrands` static class
- Pages reference predefined brands instead of constructing them

### Consequences

**Positive:**
- Eliminates duplication
- Centralizes branding definitions
- Makes feature identity explicit

**Tradeoffs:**
- Slight abstraction layer added

---

## ADR-003: Feature-Based UI Ownership Model

**Date:** July 2, 2026

### Decision

Features are responsible for defining their UI context (theme, branding, etc.)

### Context

Pages previously operated independently with inconsistent UI state handling.

### Decision

Each feature may:
- Set footer branding
- Influence theme (if needed)
- Define UI context via AppState

### Consequences

**Positive:**
- Features feel self-contained
- Easier onboarding of new features
- Cleaner separation of concerns

**Tradeoffs:**
- Requires discipline to avoid cross-feature state leakage

---

## ADR-004: Removal of Redundant UI State Services

**Date:** July 2, 2026

### Decision

Eliminate:
- ThemeService
- FooterBrandService

in favor of AppState.

### Context

Multiple services were managing overlapping UI state.

### Decision

All UI state flows through AppState.

### Consequences

**Positive:**
- Simplified architecture
- Reduced duplication
- Easier debugging

**Tradeoffs:**
- Requires migration effort
