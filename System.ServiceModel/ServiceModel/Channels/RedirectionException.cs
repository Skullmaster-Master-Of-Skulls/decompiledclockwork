using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000983 RID: 2435
	[Serializable]
	public class RedirectionException : CommunicationException
	{
		// Token: 0x06005E38 RID: 24120 RVA: 0x0015CD60 File Offset: 0x0015AF60
		public RedirectionException(RedirectionType type, RedirectionDuration duration, RedirectionScope scope, params RedirectionLocation[] locations) : this(RedirectionException.GetDefaultMessage(type, locations), type, duration, scope, null, locations)
		{
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x0015CD76 File Offset: 0x0015AF76
		public RedirectionException(RedirectionType type, RedirectionDuration duration, RedirectionScope scope, Exception innerException, params RedirectionLocation[] locations) : this(RedirectionException.GetDefaultMessage(type, locations), type, duration, scope, innerException, locations)
		{
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x0015CD8D File Offset: 0x0015AF8D
		public RedirectionException(string message, RedirectionType type, RedirectionDuration duration, RedirectionScope scope, params RedirectionLocation[] locations) : this(message, type, duration, scope, null, locations)
		{
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x0015CDA0 File Offset: 0x0015AFA0
		public RedirectionException(string message, RedirectionType type, RedirectionDuration duration, RedirectionScope scope, Exception innerException, params RedirectionLocation[] locations) : base(message, innerException)
		{
			if (type == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("type");
			}
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (message.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("message", SR.GetString("ParameterCannotBeEmpty"));
			}
			if (type.InternalType == RedirectionType.InternalRedirectionType.UseIntermediary || type.InternalType == RedirectionType.InternalRedirectionType.Resource)
			{
				if (locations == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("locations", SR.GetString("RedirectMustProvideLocation"));
				}
				if (locations.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("locations", SR.GetString("RedirectMustProvideLocation"));
				}
			}
			if (type.InternalType == RedirectionType.InternalRedirectionType.Cache && locations != null && locations.Length != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("RedirectCacheNoLocationAllowed"));
			}
			if (locations == null)
			{
				locations = EmptyArray<RedirectionLocation>.Instance;
			}
			this.Locations = new ReadOnlyCollection<RedirectionLocation>(locations);
			this.Type = type;
			this.Scope = scope;
			this.Duration = duration;
		}

		// Token: 0x06005E3C RID: 24124 RVA: 0x0015CEA8 File Offset: 0x0015B0A8
		private RedirectionException()
		{
		}

		// Token: 0x06005E3D RID: 24125 RVA: 0x0015CEB0 File Offset: 0x0015B0B0
		protected RedirectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.Type = (RedirectionType)info.GetValue("Type", typeof(RedirectionType));
			this.Duration = (RedirectionDuration)info.GetValue("Duration", typeof(RedirectionDuration));
			this.Scope = (RedirectionScope)info.GetValue("Scope", typeof(RedirectionScope));
			RedirectionLocation[] list = (RedirectionLocation[])info.GetValue("Locations", typeof(RedirectionLocation[]));
			this.Locations = new ReadOnlyCollection<RedirectionLocation>(list);
		}

		// Token: 0x06005E3E RID: 24126 RVA: 0x0015CF4C File Offset: 0x0015B14C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Type", this.Type, typeof(RedirectionType));
			info.AddValue("Duration", this.Duration, typeof(RedirectionDuration));
			info.AddValue("Scope", this.Scope, typeof(RedirectionScope));
			info.AddValue("Locations", this.Locations.ToArray<RedirectionLocation>(), typeof(RedirectionLocation[]));
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06005E3F RID: 24127 RVA: 0x0015CFD2 File Offset: 0x0015B1D2
		// (set) Token: 0x06005E40 RID: 24128 RVA: 0x0015CFDA File Offset: 0x0015B1DA
		public RedirectionDuration Duration { get; private set; }

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x0015CFE3 File Offset: 0x0015B1E3
		// (set) Token: 0x06005E42 RID: 24130 RVA: 0x0015CFEB File Offset: 0x0015B1EB
		public IEnumerable<RedirectionLocation> Locations { get; private set; }

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x0015CFF4 File Offset: 0x0015B1F4
		// (set) Token: 0x06005E44 RID: 24132 RVA: 0x0015CFFC File Offset: 0x0015B1FC
		public RedirectionScope Scope { get; private set; }

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x06005E45 RID: 24133 RVA: 0x0015D005 File Offset: 0x0015B205
		// (set) Token: 0x06005E46 RID: 24134 RVA: 0x0015D00D File Offset: 0x0015B20D
		public RedirectionType Type { get; private set; }

		// Token: 0x06005E47 RID: 24135 RVA: 0x0015D018 File Offset: 0x0015B218
		private static string FormatLocations(RedirectionLocation[] locations)
		{
			string result = string.Empty;
			if (locations != null && locations.Length != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				for (int i = 0; i < locations.Length; i++)
				{
					if (locations[i] != null)
					{
						num++;
						if (num > 1)
						{
							stringBuilder.AppendLine();
						}
						stringBuilder.AppendFormat("    {0}", locations[i].Address.AbsoluteUri);
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06005E48 RID: 24136 RVA: 0x0015D080 File Offset: 0x0015B280
		private static string GetDefaultMessage(RedirectionType type, RedirectionLocation[] locations)
		{
			string result = string.Empty;
			if (type != null)
			{
				if (type.InternalType == RedirectionType.InternalRedirectionType.Cache)
				{
					result = SR.GetString("RedirectCache");
				}
				else if (type.InternalType == RedirectionType.InternalRedirectionType.Resource)
				{
					result = SR.GetString("RedirectResource", new object[]
					{
						RedirectionException.FormatLocations(locations)
					});
				}
				else if (type.InternalType == RedirectionType.InternalRedirectionType.UseIntermediary)
				{
					result = SR.GetString("RedirectUseIntermediary", new object[]
					{
						RedirectionException.FormatLocations(locations)
					});
				}
				else
				{
					result = SR.GetString("RedirectGenericMessage");
				}
			}
			return result;
		}
	}
}
