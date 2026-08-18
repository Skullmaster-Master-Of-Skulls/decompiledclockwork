using System;
using System.Linq;
using System.Text;

namespace AutoMapper
{
	// Token: 0x02000009 RID: 9
	public class AutoMapperMappingException : Exception
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002A2D File Offset: 0x00000C2D
		public AutoMapperMappingException()
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A35 File Offset: 0x00000C35
		public AutoMapperMappingException(string message) : base(message)
		{
			this._message = message;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A45 File Offset: 0x00000C45
		public AutoMapperMappingException(string message, Exception inner) : base(null, inner)
		{
			this._message = message;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A56 File Offset: 0x00000C56
		public AutoMapperMappingException(ResolutionContext context)
		{
			this.Context = context;
			this.Types = context.Types;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A71 File Offset: 0x00000C71
		public AutoMapperMappingException(ResolutionContext context, Exception inner) : base(null, inner)
		{
			this.Context = context;
			this.Types = context.Types;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A8E File Offset: 0x00000C8E
		public AutoMapperMappingException(ResolutionContext context, string message) : this(context)
		{
			this._message = message;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A9E File Offset: 0x00000C9E
		public AutoMapperMappingException(TypePair types)
		{
			this.Types = types;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002AAD File Offset: 0x00000CAD
		public AutoMapperMappingException(TypePair types, Exception inner) : base(null, inner)
		{
			this.Types = types;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002ABE File Offset: 0x00000CBE
		public AutoMapperMappingException(TypePair types, string message) : this(types)
		{
			this._message = message;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002ACE File Offset: 0x00000CCE
		public ResolutionContext Context { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002AD6 File Offset: 0x00000CD6
		public TypePair Types { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public override string Message
		{
			get
			{
				string text = null;
				string newLine = Environment.NewLine;
				if (this.Types != null)
				{
					text = this._message + newLine + newLine + "Mapping types:";
					text = text + newLine + string.Format("{0} -> {1}", this.Types.SourceType.Name, this.Types.DestinationType.Name);
					text = text + newLine + string.Format("{0} -> {1}", this.Types.SourceType.FullName, this.Types.DestinationType.FullName);
				}
				if (this.Context != null)
				{
					string destPath = this.GetDestPath(this.Context);
					text = string.Concat(new string[]
					{
						text,
						newLine,
						newLine,
						"Destination path:",
						newLine,
						destPath
					});
					return string.Concat(new object[]
					{
						text,
						newLine,
						newLine,
						"Source value:",
						newLine,
						this.Context.SourceValue ?? "(null)"
					});
				}
				if (this._message != null)
				{
					text = this._message;
				}
				return ((text == null) ? null : (text + newLine)) + base.Message;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C18 File Offset: 0x00000E18
		private string GetDestPath(ResolutionContext context)
		{
			ResolutionContext[] contexts = context.GetContexts();
			StringBuilder stringBuilder = new StringBuilder(contexts[0].DestinationType.Name);
			foreach (ResolutionContext resolutionContext in contexts)
			{
				if (!string.IsNullOrEmpty(resolutionContext.MemberName))
				{
					stringBuilder.Append(".");
					stringBuilder.Append(resolutionContext.MemberName);
				}
				if (resolutionContext.ArrayIndex != null)
				{
					stringBuilder.AppendFormat("[{0}]", resolutionContext.ArrayIndex);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public override string StackTrace
		{
			get
			{
				return string.Join(Environment.NewLine, from str in base.StackTrace.Split(new string[]
				{
					Environment.NewLine
				}, StringSplitOptions.None)
				where !str.TrimStart(new char[0]).StartsWith("at AutoMapper.")
				select str);
			}
		}

		// Token: 0x0400000F RID: 15
		private readonly string _message;
	}
}
