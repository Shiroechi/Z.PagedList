using Z.PagedList.Web.Common;

namespace Z.PagedList.Mvc.Core
{
	internal sealed class TagBuilderFactory : ITagBuilderFactory
	{
		public ITagBuilder Create(string tagName)
		{
			return new TagBuilder(tagName);
		}
	}
}
