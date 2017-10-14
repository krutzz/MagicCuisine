using System;

namespace Services.Providers
{
    public interface IDateProvider
    {
        DateTime GetCurrentDate();
    }
}
