using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002E6 RID: 742
	public abstract class ParseRecorder
	{
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x00070B73 File Offset: 0x0006ED73
		public static IList<Func<ParseRecorder>> RecorderFactories
		{
			get
			{
				if (ParseRecorder._factories == null)
				{
					ParseRecorder._factories = new List<Func<ParseRecorder>>();
				}
				return ParseRecorder._factories;
			}
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x00070B8C File Offset: 0x0006ED8C
		internal static ParseRecorder CreateRecorders(TemplateParser parser)
		{
			List<ParseRecorder> list = new List<ParseRecorder>();
			if (ParseRecorder._factories != null)
			{
				using (List<Func<ParseRecorder>>.Enumerator enumerator = ParseRecorder._factories.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Func<ParseRecorder> func = enumerator.Current;
						ParseRecorder parseRecorder = func();
						if (parseRecorder != null)
						{
							list.Add(parseRecorder);
						}
					}
					goto IL_61;
				}
			}
			if (!BinaryCompatibility.Current.TargetsAtLeastFramework472)
			{
				return ParseRecorder.Null;
			}
			IL_61:
			if (BinaryCompatibility.Current.TargetsAtLeastFramework472)
			{
				list.Add(new WebObjectActivatorParseRecorder());
			}
			ParseRecorder.ParseRecorderList parseRecorderList = new ParseRecorder.ParseRecorderList(list);
			parseRecorderList.Initialize(parser);
			return parseRecorderList;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void Initialize(TemplateParser parser)
		{
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void RecordBeginTag(ControlBuilder builder, Match tag)
		{
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void RecordEndTag(ControlBuilder builder, Match tag)
		{
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void RecordEmptyTag(ControlBuilder builder, Match tag)
		{
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void RecordCodeBlock(ControlBuilder builder, Match codeBlock)
		{
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ParseComplete(ControlBuilder root)
		{
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ProcessGeneratedCode(ControlBuilder builder, CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
		{
		}

		// Token: 0x04001C46 RID: 7238
		internal static readonly ParseRecorder Null = new ParseRecorder.NullParseRecorder();

		// Token: 0x04001C47 RID: 7239
		private static List<Func<ParseRecorder>> _factories;

		// Token: 0x02000981 RID: 2433
		private sealed class NullParseRecorder : ParseRecorder
		{
		}

		// Token: 0x02000982 RID: 2434
		private sealed class ParseRecorderList : ParseRecorder
		{
			// Token: 0x06006A3B RID: 27195 RVA: 0x0017B7EA File Offset: 0x001799EA
			internal ParseRecorderList(IEnumerable<ParseRecorder> recorders)
			{
				this._recorders = recorders;
			}

			// Token: 0x06006A3C RID: 27196 RVA: 0x0017B7FC File Offset: 0x001799FC
			public override void Initialize(TemplateParser parser)
			{
				foreach (ParseRecorder parseRecorder in this._recorders)
				{
					parseRecorder.Initialize(parser);
				}
			}

			// Token: 0x06006A3D RID: 27197 RVA: 0x0017B84C File Offset: 0x00179A4C
			public override void RecordBeginTag(ControlBuilder builder, Match tag)
			{
				foreach (ParseRecorder parseRecorder in this._recorders)
				{
					parseRecorder.RecordBeginTag(builder, tag);
				}
			}

			// Token: 0x06006A3E RID: 27198 RVA: 0x0017B89C File Offset: 0x00179A9C
			public override void RecordEndTag(ControlBuilder builder, Match tag)
			{
				foreach (ParseRecorder parseRecorder in this._recorders)
				{
					parseRecorder.RecordEndTag(builder, tag);
				}
			}

			// Token: 0x06006A3F RID: 27199 RVA: 0x0017B8EC File Offset: 0x00179AEC
			public override void RecordEmptyTag(ControlBuilder builder, Match tag)
			{
				foreach (ParseRecorder parseRecorder in this._recorders)
				{
					parseRecorder.RecordEmptyTag(builder, tag);
				}
			}

			// Token: 0x06006A40 RID: 27200 RVA: 0x0017B93C File Offset: 0x00179B3C
			public override void RecordCodeBlock(ControlBuilder builder, Match codeBlock)
			{
				foreach (ParseRecorder parseRecorder in this._recorders)
				{
					parseRecorder.RecordCodeBlock(builder, codeBlock);
				}
			}

			// Token: 0x06006A41 RID: 27201 RVA: 0x0017B98C File Offset: 0x00179B8C
			public override void ParseComplete(ControlBuilder root)
			{
				foreach (ParseRecorder parseRecorder in this._recorders)
				{
					parseRecorder.ParseComplete(root);
				}
			}

			// Token: 0x06006A42 RID: 27202 RVA: 0x0017B9DC File Offset: 0x00179BDC
			public override void ProcessGeneratedCode(ControlBuilder builder, CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
			{
				foreach (ParseRecorder parseRecorder in this._recorders)
				{
					parseRecorder.ProcessGeneratedCode(builder, codeCompileUnit, baseType, derivedType, buildMethod, dataBindingMethod);
				}
			}

			// Token: 0x040038BA RID: 14522
			private readonly IEnumerable<ParseRecorder> _recorders;
		}
	}
}
