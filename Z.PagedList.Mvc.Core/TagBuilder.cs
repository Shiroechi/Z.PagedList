using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Html;

using Z.PagedList.Web.Common;

namespace Z.PagedList.Mvc.Core
{
	internal sealed class TagBuilder : ITagBuilder
	{
		private readonly Microsoft.AspNetCore.Mvc.Rendering.TagBuilder _tagBuilder;

		public TagBuilder(string tagName)
		{
			this._tagBuilder = new Microsoft.AspNetCore.Mvc.Rendering.TagBuilder(tagName);
			this.Attributes = this._tagBuilder.Attributes;
		}

		public IDictionary<string, string> Attributes { get; }

		public void AddCssClass(string value)
		{
			this._tagBuilder
				.AddCssClass(value);
		}

		public void AppendHtml(string innerHtml)
		{
			this._tagBuilder.InnerHtml
				.AppendHtml(innerHtml);
		}

		public void MergeAttribute(string key, string value)
		{
			this._tagBuilder
				.MergeAttribute(key, value);
		}

		public void SetInnerText(string innerText)
		{
			this._tagBuilder.InnerHtml
				.SetContent(innerText);
		}

		public string ToString(TagRenderMode renderMode, HtmlEncoder encoder = null)
		{
			encoder = HtmlEncoder.Create(new TextEncoderSettings());

			using (var writer = new StringWriter() as TextWriter)
			{
				this._tagBuilder.WriteTo(writer, encoder);

				return writer.ToString();
			}
		}

		public override string ToString()
		{
			return this._tagBuilder
				.ToString();
		}
	}
}
