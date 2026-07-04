🎧 AppState and Feature Context — How BlazorExpo Thinks
A narrative walkthrough of how state and features work together

After establishing Foundation 2.0, one thing became clear.

We had solved the problem of scattered state.

But we had not yet fully solved the problem of who is responsible for state at any given moment.

Because in a real application, state does not just exist.

It changes depending on where the user is.

This is where AppState becomes more than just storage.

It becomes the center of coordination for the user interface.

Not just holding values.

But representing what the application currently is.

At any moment, the application has a context.

That context might be:

Home.

Weather.

Movies.

Themes.

Or something else entirely in the future.

Each of these is not just a page.

It is a feature context.

A feature context defines how the application should look and behave while that feature is active.

For example:

When the Weather feature is active, the footer shows Weather branding.

When the Movies feature is active, the footer reflects Movie branding.

When Themes is active, it may reset or display a neutral state.

So instead of thinking:

“This page sets the footer.”

We now think:

“This feature defines the UI context.”

That is a subtle but important shift.

Because pages come and go.

But features define meaning.

AppState is the mechanism that carries this meaning.

It holds the current UI state, such as:

The selected theme.

The active footer brand.

And any other shared visual state.

But it does not decide what those values should be.

It only stores and broadcasts them.

When a feature becomes active, it updates AppState.

For example:

The Weather feature tells AppState:

“I am now active, and my branding is NWS.”

The Movies feature says:

“I am now active, and my branding is MovieDB.”

AppState receives these updates and notifies the system.

Any component that depends on this state reacts automatically.

The footer updates.

The layout updates.

And anything else listening responds immediately.

This creates a clean flow of responsibility.

Features define intent.

AppState stores state.

UI components react.

There is no guessing.

There is no shared mutation from random places.

And there is no hidden dependency on navigation order.

But this system only works if one rule is respected.

AppState must always start valid.

There must never be a moment where the UI is asking:

“What should I render?”

and AppState does not have an answer.

That is why default values exist.

They are not placeholders.

They are guarantees.

The application always begins in a known state.

Once that foundation is stable, a second rule becomes important.

Features must explicitly declare their context when they become active.

Not implicitly.

Not indirectly.

But clearly.

This is what prevents state leakage between pages.

Because without explicit context changes, state would carry over from the previous feature.

And that is exactly the kind of subtle bug that appears as “random UI behavior.”

So now the lifecycle becomes predictable.

A feature is entered.

It sets its context in AppState.

The UI reacts.

The user sees consistent behavior.

Then the feature is left.

And the next feature takes over responsibility.

This model also scales naturally.

Because adding a new feature does not require modifying existing ones.

It only requires defining:

What does this feature set in AppState when it becomes active?

That is it.

No global services.

No shared mutation chains.

No hidden initialization order.

Just explicit feature ownership of UI context.

Over time, this creates a system that behaves like a framework.

Not because it was designed to be one.

But because the rules are consistent enough that patterns emerge.

And those patterns are simple.

AppState owns state.

Features define context.

Components render UI.

Once that structure is in place, the application becomes predictable.

And predictability is what allows complexity to grow without breaking the system.

This is the real goal of BlazorExpo now.

Not just to build pages.

Not just to integrate features.

But to build a system where every new feature fits cleanly into an existing mental model.

Because when that happens, development stops feeling like patchwork.

And starts feeling like extension.

And that is the difference between an application that grows messy over time…

and an application that can grow indefinitely.
