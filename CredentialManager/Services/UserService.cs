using System.Text;
using CredentialManager.Models;
using CredentialManager.Database;
using CredentialManager.Utils;
using CredentialManager.Database.Repositories;
using System.ComponentModel.DataAnnotations;
using Spectre.Console;

namespace CredentialManager.Services;

public class UserService
{
    private readonly UserRepository _repo = new();

    public bool IsUserRegistered()
    {
        // if second 'if' expression is evaluated, then ReadAll should NOT have returned null
        return _repo.ReadAll() is not null && _repo.ReadAll().Count > 0;
    }
    
    public bool CheckLogin(User user) => LogIn(user);

    public void Register(User user)
    {
        // validate email address
        while (!ValidateEmail(user.Email))
        {
            // continue the loop to prompt user until an email of valid format is given
            // right now, this ONLY checks if the email is of a valid format, not if the email is an existing address
            AnsiConsole.MarkupLine($"[bold red]Warning: '{user.Email}' does not appear to be a valid address.[/]\n");
            user.Email = AnsiConsole.Prompt(
                new TextPrompt<string>("Please re-enter your email:")
                );
        }  

        _repo.Create(user);
    }

    public bool ValidateEmail(string email)
    {
        EmailAddressAttribute emailValidator = new();
        return email != "" && emailValidator.IsValid(email);
    }

    bool LogIn(User user)
    {
        var results = _repo.ReadAll();
        if (results != null && results.Count > 0)
        {
            return results.First().Username == user.Username &&
                results.First().Password == user.Password;
        }

        return false;
    }
}