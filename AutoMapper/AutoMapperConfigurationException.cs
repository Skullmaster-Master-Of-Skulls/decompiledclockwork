using System;
using System.Linq;
using System.Text;

namespace AutoMapper
{
	// Token: 0x02000008 RID: 8
	public class AutoMapperConfigurationException : Exception
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002712 File Offset: 0x00000912
		public AutoMapperConfigurationException.TypeMapConfigErrors[] Errors { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000271A File Offset: 0x0000091A
		public ResolutionContext Context { get; }

		// Token: 0x06000027 RID: 39 RVA: 0x00002722 File Offset: 0x00000922
		public AutoMapperConfigurationException(string message) : base(message)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000272B File Offset: 0x0000092B
		protected AutoMapperConfigurationException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002735 File Offset: 0x00000935
		public AutoMapperConfigurationException(AutoMapperConfigurationException.TypeMapConfigErrors[] errors)
		{
			this.Errors = errors;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002744 File Offset: 0x00000944
		public AutoMapperConfigurationException(ResolutionContext context)
		{
			this.Context = context;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002754 File Offset: 0x00000954
		public override string Message
		{
			get
			{
				if (this.Context != null)
				{
					ResolutionContext resolutionContext = this.Context;
					string str = string.Format("The following property on {0} cannot be mapped: \n\t{2}\nAdd a custom mapping expression, ignore, add a custom resolver, or modify the destination type {1}.", resolutionContext.DestinationType.FullName, resolutionContext.DestinationType.FullName, resolutionContext.GetContextPropertyMap().DestinationProperty.Name);
					str += "\nContext:";
					while (resolutionContext != null)
					{
						str += ((resolutionContext.GetContextPropertyMap() == null) ? string.Format("\n\tMapping from type {1} to {0}", resolutionContext.DestinationType.FullName, resolutionContext.SourceType.FullName) : string.Format("\n\tMapping to property {0} from {2} to {1}", resolutionContext.GetContextPropertyMap().DestinationProperty.Name, resolutionContext.DestinationType.FullName, resolutionContext.SourceType.FullName));
						resolutionContext = resolutionContext.Parent;
					}
					return str + "\n" + base.Message;
				}
				if (this.Errors != null)
				{
					StringBuilder stringBuilder = new StringBuilder("\nUnmapped members were found. Review the types and members below.\nAdd a custom mapping expression, ignore, add a custom resolver, or modify the source/destination type\n");
					foreach (AutoMapperConfigurationException.TypeMapConfigErrors typeMapConfigErrors in this.Errors)
					{
						int count = typeMapConfigErrors.TypeMap.SourceType.FullName.Length + typeMapConfigErrors.TypeMap.DestinationType.FullName.Length + 5;
						stringBuilder.AppendLine(new string('=', count));
						stringBuilder.AppendLine(string.Concat(new object[]
						{
							typeMapConfigErrors.TypeMap.SourceType.Name,
							" -> ",
							typeMapConfigErrors.TypeMap.DestinationType.Name,
							" (",
							typeMapConfigErrors.TypeMap.ConfiguredMemberList,
							" member list)"
						}));
						stringBuilder.AppendLine(string.Concat(new object[]
						{
							typeMapConfigErrors.TypeMap.SourceType.FullName,
							" -> ",
							typeMapConfigErrors.TypeMap.DestinationType.FullName,
							" (",
							typeMapConfigErrors.TypeMap.ConfiguredMemberList,
							" member list)"
						}));
						stringBuilder.AppendLine();
						stringBuilder.AppendLine("Unmapped properties:");
						foreach (string value in typeMapConfigErrors.UnmappedPropertyNames)
						{
							stringBuilder.AppendLine(value);
						}
					}
					return stringBuilder.ToString();
				}
				return base.Message;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000029C4 File Offset: 0x00000BC4
		public override string StackTrace
		{
			get
			{
				if (this.Errors != null)
				{
					return string.Join(Environment.NewLine, (from str in base.StackTrace.Split(new string[]
					{
						Environment.NewLine
					}, StringSplitOptions.None)
					where !str.TrimStart(new char[0]).StartsWith("at AutoMapper.")
					select str).ToArray<string>());
				}
				return base.StackTrace;
			}
		}

		// Token: 0x020000C0 RID: 192
		public class TypeMapConfigErrors
		{
			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00015291 File Offset: 0x00013491
			public TypeMap TypeMap { get; }

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00015299 File Offset: 0x00013499
			public string[] UnmappedPropertyNames { get; }

			// Token: 0x060005A9 RID: 1449 RVA: 0x000152A1 File Offset: 0x000134A1
			public TypeMapConfigErrors(TypeMap typeMap, string[] unmappedPropertyNames)
			{
				this.TypeMap = typeMap;
				this.UnmappedPropertyNames = unmappedPropertyNames;
			}
		}
	}
}
