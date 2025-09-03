using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CredentialManager.Services;

namespace CredentialManagerTests;
public class UserServiceUnitTest
{
    private readonly UserService us = new();

    [Fact]
    public void EmailWithFullyQualifiedDomainNameIsValid()
    {
        var email = "testemailaccount@gmail.com";
        Assert.True(us.ValidateEmail(email));
    }

    [Fact]
    public void EmailWithoutTLDIsNotValid()
    {
        var email = "estemailaccount@";
        Assert.False(us.ValidateEmail(email));
    }

    [Fact]
    public void EmailWithNonstandardTLDIsValid()
    {
        var email = "testaccount@thisismadeup.deez";
        Assert.True(us.ValidateEmail(email));
    }
}
