using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pizzeria.Services;

namespace Pizzeria.Config;

public static class MyUtils
{
    private static OperationResult ValidateModel(ModelStateDictionary modelState)
    {
        var operationResult = new OperationResult();

        if (!modelState.IsValid)
        {
            operationResult.Success = false;

            foreach (var state in modelState)
            {
                if (state.Value.Errors.Any())
                {
                    foreach (var error in state.Value.Errors)
                    {
                        operationResult.Errors.Add(new FieldError
                        {
                            FieldKey = state.Key,
                            ErrorMsg = error.ErrorMessage
                        });
                    }
                }
            }
        }

        return operationResult;
    }
    
    public static OperationResult ValidateModel<T>(T obj) where T : class
    {
        var modelState = new ModelStateDictionary();
        var validationResults = new List<ValidationResult>();

        var context = new ValidationContext(obj, null, null);
        bool isValid = Validator.TryValidateObject(obj, context, validationResults, true);

        foreach (var validationResult in validationResults)
        {
            modelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? "", validationResult.ErrorMessage);
        }

        OperationResult result = ValidateModel(modelState);

        return result;
    }
}