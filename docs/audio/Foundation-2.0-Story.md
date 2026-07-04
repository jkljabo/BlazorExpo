🎧 Foundation 2.0 — The BlazorExpo Story
A narrative walkthrough of how the architecture came together

When BlazorExpo started, it was just a collection of pages.

Each page did its own thing.

Themes controlled their own behavior.

Weather had its own idea of branding.

The footer managed its own state.

And everything worked—until it didn’t.

Because as the application grew, something subtle started happening.

The same information existed in multiple places.

And those places didn’t always agree.

At first, these inconsistencies were small.

A footer image would not match the current page.

A theme would persist when it shouldn’t.

A refresh would “fix” the UI, but only temporarily.

It felt random at first.

But it wasn’t random.

It was a structure problem.

So we made a decision.

A foundational decision.

We asked a simple question:

What if there was exactly one place in the application that owned shared UI state?

That question led to AppState.

AppState became the single source of truth for how the application looks and feels at any moment.

It holds things like:

The current theme.

The current footer branding.

And eventually, other UI-level state like layout and navigation context.

But AppState is not just storage.

It is the authority.

Once AppState was introduced, everything started to change.

Instead of each page managing its own version of state, pages began telling AppState what the UI should be.

And the UI began reacting to AppState.

Not the other way around.

This shift sounds small.

But it changed the entire mental model of the application.

We stopped thinking in terms of pages controlling themselves.

And started thinking in terms of a shared UI state driving the entire experience.

But this introduced a new requirement.

AppState could no longer be “partially initialized.”

Because if AppState is the source of truth, then it must always be valid.

There cannot be a moment where it is undefined.

There cannot be a moment where the UI asks for data that doesn’t exist.

So we introduced a new rule:

AppState must always start in a valid state.

That’s why default values matter.

The footer brand cannot be null.

The theme cannot be undefined.

The system must always know what it is supposed to render.

Because Blazor renders immediately.

There is no “pause” for initialization.

Once this became consistent, another issue surfaced.

Navigation was no longer resetting state.

And that exposed a subtle bug.

Some pages assumed they were the first thing to run.

Some pages implicitly initialized state.

Others did not.

So state would sometimes carry over unexpectedly.

This was not a failure of AppState.

It was a signal that initialization responsibility was unclear.

So we refined the rule again.

Each feature is responsible for declaring its UI context when it becomes active.

Not globally.

Not randomly.

But explicitly.

This means that when you navigate into Weather, the Weather feature declares its branding.

When you navigate into Movies, Movies declares its own context.

And when you navigate into Themes, Themes resets or defines what it needs.

This removes ambiguity.

And it makes navigation predictable again.

Then came another lesson.

Even with AppState in place, the UI could still fail if it assumed too much.

So components like the footer had to become defensive.

They could no longer assume values existed.

They had to handle the system gracefully even if something went wrong.

Because reliability is not just about architecture.

It is also about resilience.

Over time, a pattern emerged.

Three rules began to define BlazorExpo:

First.

AppState owns shared UI state.

Second.

Features define what that state should be.

Third.

Components render UI based on that state.

This separation created clarity.

Instead of multiple services competing for control, there is now a single authority.

Instead of pages guessing what they should do, they declare intent.

And instead of UI logic being scattered, it is centralized and predictable.

This is the foundation of BlazorExpo.

Not the pages.

Not the features.

But the structure that allows them to exist without conflict.

And this is why Foundation 2.0 matters.

Because once this system is in place, adding a new feature is no longer an architectural decision.

It becomes a repeatable pattern.

You define the feature.

You define its UI context.

You plug it into AppState.

And the rest of the system adapts.

That is the goal.

A system where growth does not increase complexity.

A system where new features do not break old ones.

A system where the structure supports evolution instead of resisting it.

And this is where BlazorExpo is now.

Not at the beginning.

Not at the end.

But at the point where the architecture starts to matter more than the individual pages.

Foundation 2.0 is not a feature milestone.

It is a shift in thinking.

From pages that own themselves.

To a system that owns its state.

And from here, everything gets easier.

Because now, we are not just building a UI.

We are building a predictable system for UI behavior.
