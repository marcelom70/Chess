using Chess.Domain.Entities;
using EasyArchitecture.Plugins.BultIn.Translation;

namespace Chess.Infrastructure.Translation
{
    public class CustomMap : MapRule
    {
        public CustomMap()
        {
            AddMapRule<string, Path>((source, target) => new Path(source));
        }
    }

}
