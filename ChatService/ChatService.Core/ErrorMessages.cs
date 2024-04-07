namespace ChatService.Core;
public sealed class ErrorMessages
{
    public const string KeyIsNull = "{0} key is null";
    public const string NotFound = "{0} not found";

    public const string Adding = "Adding {0}";
    public const string Removing = "Removing {0}";

    public const string UserIsTheFounder = "User is the founder";
    public const string FounderCanNotBeRemoved = "Founder can not be removed";

    public const string GroupHasNoUsers = "Group has no users";

    public const string UserHasNoRoles = "User has no roles";
    public const string UserNotFoundOnGroup = "User {0} Not Found On Group {1}";

    public const string SomethingWentWrong = "Something went wrong";
}

