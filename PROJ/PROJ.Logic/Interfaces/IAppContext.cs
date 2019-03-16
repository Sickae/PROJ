using System.Collections.Generic;

namespace PROJ.Logic.Interfaces
{
    public interface IAppContext
    {
        int? UserId { get; }
        IEnumerable<int> ProjectIds { get; }
    }
}
