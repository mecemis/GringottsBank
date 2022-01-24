using System;

namespace GringottsBank.Shared.Models.Identity
{
    public static class CurrentCustomer
    {
        public static Guid Id { get; set; }
        public static string Email { get; set; }
        public static string UserName { get; set; }
        public static string FullName { get; set; }
        public static bool IsAuthenticated { get; set; }
    }
}