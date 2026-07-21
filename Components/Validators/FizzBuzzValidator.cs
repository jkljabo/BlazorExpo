using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Components.Validators
{
    public class FizzBuzzValidator : ComponentBase
    {
        private ValidationMessageStore validationMessageStore = null!;

        [CascadingParameter]
        private EditContext CurrentEditContext { get; set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(FizzBuzzValidator)} requires a cascading parameter of type {nameof(EditContext)}. For example, you can use {nameof(FizzBuzzValidator)} inside an {nameof(EditForm)}.");
            }

            validationMessageStore = new(CurrentEditContext);

            // Attach methods to validation events.
            CurrentEditContext.OnFieldChanged += (s, e) => ValidateField(e.FieldIdentifier);
            CurrentEditContext.OnValidationRequested += (s, e) => ValidateAllField();
        }

        private void ValidateField(FieldIdentifier fieldIdentifier)
        {
            var fizzbuzz = CurrentEditContext.Model as FizzBuzzModel ?? throw new InvalidOperationException($"{nameof(FizzBuzzValidator)} requires a model of type FizzBuzzModel.");

            // Clears previous errors for this field.
            validationMessageStore.Clear(fieldIdentifier);

            // Validate the field.
            if (fieldIdentifier.FieldName == nameof(FizzBuzzModel.FizzValue)) 
            {
                if (fizzbuzz.FizzValue >= fizzbuzz.BuzzValue)
                {
                    validationMessageStore.Add(fieldIdentifier, "The fizz value must be less than the buzz value.");
                }
            }
            else if(fieldIdentifier.FieldName == nameof(FizzBuzzModel.BuzzValue))
            {
                if (fizzbuzz.BuzzValue <= fizzbuzz.FizzValue)
                {
                    validationMessageStore.Add(fieldIdentifier, "The buzz value must be greater than the fizz value.");
                }
            }
            else if (fieldIdentifier.FieldName == nameof(FizzBuzzModel.StopValue))
            {
                int requiredStopValue = fizzbuzz.FizzValue * fizzbuzz.BuzzValue;

                if (fizzbuzz.StopValue < requiredStopValue)
                {
                    validationMessageStore.Add(fieldIdentifier, $"The Stop value must be greater than or equal to {requiredStopValue}.");
                }
            }
        }

        private void ValidateAllField()
        {
            var fizzbuzz = CurrentEditContext.Model as FizzBuzzModel ?? throw new InvalidOperationException($"{nameof(FizzBuzzValidator)} requires a model of type FizzBuzzModel.");

            // Clear all validation errors.
            validationMessageStore.Clear();

            // Validate all fields.
            ValidateField(new FieldIdentifier(fizzbuzz, nameof(FizzBuzzModel.FizzValue)));
            ValidateField(new FieldIdentifier(fizzbuzz, nameof(FizzBuzzModel.BuzzValue)));
            ValidateField(new FieldIdentifier(fizzbuzz, nameof(FizzBuzzModel.StopValue)));

            // Notify the edit context of the validation state change.
            CurrentEditContext.NotifyValidationStateChanged();
        }
    }
}
