🎧 Feature Lifecycle in BlazorExpo
From navigation to rendered UI

When you use BlazorExpo, everything begins the same way.

A user clicks a link.

A button.

Or a navigation item in the menu.

At first glance, it feels simple.

But under the surface, a sequence of events begins that defines how the entire application behaves.

The first step is navigation.

Blazor’s router determines which component should be rendered.

For example:

WeatherDashboard.

Movies.

Themes.

Or Home.

At this moment, the application has not yet changed its state.

It has only changed its destination.

Once the new page is selected, Blazor begins constructing the component.

This is where the feature lifecycle truly starts.

The component is created.

Its lifecycle methods begin to execute.

And this is where the feature declares itself.

In Foundation 2.0 architecture, this is the critical moment.

The feature becomes active.

And it must tell the application what it represents.

It does this by updating AppState.

For example, when WeatherDashboard loads, it might say:

“This feature uses Weather branding.”

When Movies loads, it might say:

“This feature uses Movie branding.”

When Themes loads, it might reset or define a neutral context.

This is not optional behavior.

It is the feature asserting its identity into the application.

Because without this step, the system would have no way to know what context it is currently in.

Once AppState is updated, the next phase begins automatically.

AppState broadcasts that something has changed.

This is not a manual process.

It is event-driven.

Any component that depends on AppState is notified immediately.

The footer is one of those components.

It listens for changes.

When AppState updates, the footer re-evaluates its rendering logic.

It retrieves the current footer brand.

And it updates the UI accordingly.

The same pattern applies to other UI elements.

The header.

The theme system.

And any future layout components.

They all respond to AppState in the same way.

This creates a consistent loop.

Navigation triggers a feature.

A feature updates AppState.

AppState notifies the UI.

The UI re-renders.

There is no direct communication between pages and UI components.

Pages do not “reach into” the layout.

They only declare intent through AppState.

This separation is important.

Because it removes hidden dependencies.

No component needs to know which page caused a change.

It only needs to know what the current state is.

Once the UI has updated, the lifecycle is complete.

The user sees the new feature.

The correct theme is applied.

The correct branding is displayed.

And the application is in a consistent state.

This cycle repeats every time the user navigates.

And over time, it becomes predictable.

Not because it is simple.

But because it is consistent.

There is one important requirement that makes this system work.

AppState must always be valid.

At no point during this lifecycle can the UI encounter missing or undefined state.

Because rendering begins immediately after navigation.

There is no pause for initialization.

This is why defaults exist inside AppState.

They are not fallback behavior.

They are guarantees that the lifecycle can always complete successfully.

When this model is followed correctly, something important happens.

The application stops depending on timing.

It stops depending on page load order.

And it stops depending on accidental initialization.

Instead, everything becomes explicit.

Navigation is explicit.

Feature activation is explicit.

State changes are explicit.

And UI updates are reactive.

This is the core idea behind BlazorExpo’s architecture.

Not pages controlling themselves.

But a system reacting to declared state.

Once this pattern is established, new features become predictable.

A new feature does not need to understand the entire system.

It only needs to answer one question:

“What should AppState look like when this feature is active?”

And once that question is answered, everything else follows automatically.

The UI updates.

The layout adjusts.

And the application remains consistent.

This is what turns BlazorExpo from a collection of pages into a coordinated system.

Not because it is complex.

But because the flow of responsibility is clear.

Navigation leads to features.

Features define state.

State drives the UI.

That is the full lifecycle.

And once you understand that cycle, every part of the application becomes easier to reason about.
