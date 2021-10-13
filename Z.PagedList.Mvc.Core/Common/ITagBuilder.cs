﻿using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace Z.PagedList.Web.Common
{
	public interface ITagBuilder
	{
		IDictionary<string, string> Attributes { get; }

		void AppendHtml(string innerHtml);

		void AddCssClass(string value);

		void MergeAttribute(string key, string value);

		void SetInnerText(string innerText);

		string ToString(TagRenderMode renderMode, HtmlEncoder encoder = null);
	}
}