using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200009C RID: 156
	[SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "The standard constructors don't make sense for this exception, as calling them will leave out the information that makes the exception useful in the first place.")]
	public class ResolutionFailedException : Exception
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000909B File Offset: 0x0000729B
		public ResolutionFailedException(Type typeRequested, string nameRequested, Exception innerException, IBuilderContext context) : base(ResolutionFailedException.CreateMessage(typeRequested, nameRequested, innerException, context), innerException)
		{
			Guard.ArgumentNotNull(typeRequested, "typeRequested");
			if (typeRequested != null)
			{
				this.typeRequested = typeRequested.GetTypeInfo().Name;
			}
			this.nameRequested = nameRequested;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000090D4 File Offset: 0x000072D4
		public string TypeRequested
		{
			get
			{
				return this.typeRequested;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000090DC File Offset: 0x000072DC
		public string NameRequested
		{
			get
			{
				return this.nameRequested;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000090E4 File Offset: 0x000072E4
		private static string CreateMessage(Type typeRequested, string nameRequested, Exception innerException, IBuilderContext context)
		{
			Guard.ArgumentNotNull(typeRequested, "typeRequested");
			Guard.ArgumentNotNull(context, "context");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.CurrentCulture, Resources.ResolutionFailed, new object[]
			{
				typeRequested,
				ResolutionFailedException.FormatName(nameRequested),
				ResolutionFailedException.ExceptionReason(context),
				(innerException != null) ? innerException.GetType().GetTypeInfo().Name : "ResolutionFailedException",
				(innerException != null) ? innerException.Message : null
			});
			stringBuilder.AppendLine();
			ResolutionFailedException.AddContextDetails(stringBuilder, context, 1);
			return stringBuilder.ToString();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00009180 File Offset: 0x00007380
		private static void AddContextDetails(StringBuilder builder, IBuilderContext context, int depth)
		{
			if (context != null)
			{
				string value = new string(' ', depth * 2);
				NamedTypeBuildKey buildKey = context.BuildKey;
				NamedTypeBuildKey originalBuildKey = context.OriginalBuildKey;
				builder.Append(value);
				if (buildKey == originalBuildKey)
				{
					builder.AppendFormat(CultureInfo.CurrentCulture, Resources.ResolutionTraceDetail, new object[]
					{
						buildKey.Type,
						ResolutionFailedException.FormatName(buildKey.Name)
					});
				}
				else
				{
					builder.AppendFormat(CultureInfo.CurrentCulture, Resources.ResolutionWithMappingTraceDetail, new object[]
					{
						buildKey.Type,
						ResolutionFailedException.FormatName(buildKey.Name),
						originalBuildKey.Type,
						ResolutionFailedException.FormatName(originalBuildKey.Name)
					});
				}
				builder.AppendLine();
				if (context.CurrentOperation != null)
				{
					builder.Append(value);
					builder.AppendFormat(CultureInfo.CurrentCulture, context.CurrentOperation.ToString(), new object[0]);
					builder.AppendLine();
				}
				ResolutionFailedException.AddContextDetails(builder, context.ChildContext, depth + 1);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00009286 File Offset: 0x00007486
		private static string FormatName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return name;
			}
			return "(none)";
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00009298 File Offset: 0x00007498
		private static string ExceptionReason(IBuilderContext context)
		{
			IBuilderContext builderContext = context;
			while (builderContext.ChildContext != null)
			{
				builderContext = builderContext.ChildContext;
			}
			if (builderContext.CurrentOperation != null)
			{
				return builderContext.CurrentOperation.ToString();
			}
			return Resources.NoOperationExceptionReason;
		}

		// Token: 0x0400009D RID: 157
		private string typeRequested;

		// Token: 0x0400009E RID: 158
		private string nameRequested;
	}
}
