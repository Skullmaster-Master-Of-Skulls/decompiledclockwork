using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007CF RID: 1999
	[ComVisible(true)]
	public sealed class BinaryFormatter : IRemotingFormatter, IFormatter
	{
		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x000F0B14 File Offset: 0x000EFB14
		// (set) Token: 0x060046A3 RID: 18083 RVA: 0x000F0B1C File Offset: 0x000EFB1C
		public FormatterTypeStyle TypeFormat
		{
			get
			{
				return this.m_typeFormat;
			}
			set
			{
				this.m_typeFormat = value;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x060046A4 RID: 18084 RVA: 0x000F0B25 File Offset: 0x000EFB25
		// (set) Token: 0x060046A5 RID: 18085 RVA: 0x000F0B2D File Offset: 0x000EFB2D
		public FormatterAssemblyStyle AssemblyFormat
		{
			get
			{
				return this.m_assemblyFormat;
			}
			set
			{
				this.m_assemblyFormat = value;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x060046A6 RID: 18086 RVA: 0x000F0B36 File Offset: 0x000EFB36
		// (set) Token: 0x060046A7 RID: 18087 RVA: 0x000F0B3E File Offset: 0x000EFB3E
		public TypeFilterLevel FilterLevel
		{
			get
			{
				return this.m_securityLevel;
			}
			set
			{
				this.m_securityLevel = value;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x060046A8 RID: 18088 RVA: 0x000F0B47 File Offset: 0x000EFB47
		// (set) Token: 0x060046A9 RID: 18089 RVA: 0x000F0B4F File Offset: 0x000EFB4F
		public ISurrogateSelector SurrogateSelector
		{
			get
			{
				return this.m_surrogates;
			}
			set
			{
				this.m_surrogates = value;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060046AA RID: 18090 RVA: 0x000F0B58 File Offset: 0x000EFB58
		// (set) Token: 0x060046AB RID: 18091 RVA: 0x000F0B60 File Offset: 0x000EFB60
		public SerializationBinder Binder
		{
			get
			{
				return this.m_binder;
			}
			set
			{
				this.m_binder = value;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060046AC RID: 18092 RVA: 0x000F0B69 File Offset: 0x000EFB69
		// (set) Token: 0x060046AD RID: 18093 RVA: 0x000F0B71 File Offset: 0x000EFB71
		public StreamingContext Context
		{
			get
			{
				return this.m_context;
			}
			set
			{
				this.m_context = value;
			}
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000F0B7A File Offset: 0x000EFB7A
		public BinaryFormatter()
		{
			this.m_surrogates = null;
			this.m_context = new StreamingContext(StreamingContextStates.All);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000F0BA7 File Offset: 0x000EFBA7
		public BinaryFormatter(ISurrogateSelector selector, StreamingContext context)
		{
			this.m_surrogates = selector;
			this.m_context = context;
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000F0BCB File Offset: 0x000EFBCB
		public object Deserialize(Stream serializationStream)
		{
			return this.Deserialize(serializationStream, null);
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x000F0BD5 File Offset: 0x000EFBD5
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck)
		{
			return this.Deserialize(serializationStream, null, fCheck, null);
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x000F0BE1 File Offset: 0x000EFBE1
		public object Deserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.Deserialize(serializationStream, handler, true, null);
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x000F0BED File Offset: 0x000EFBED
		public object DeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, true, methodCallMessage);
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x000F0BF9 File Offset: 0x000EFBF9
		[ComVisible(false)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public object UnsafeDeserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.Deserialize(serializationStream, handler, false, null);
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x000F0C05 File Offset: 0x000EFC05
		[ComVisible(false)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public object UnsafeDeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, false, methodCallMessage);
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x000F0C11 File Offset: 0x000EFC11
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, fCheck, false, methodCallMessage);
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x000F0C20 File Offset: 0x000EFC20
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentNull_WithParamName"), new object[]
				{
					serializationStream
				}));
			}
			if (serializationStream.CanSeek && serializationStream.Length == 0L)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_Stream"));
			}
			InternalFE internalFE = new InternalFE();
			internalFE.FEtypeFormat = this.m_typeFormat;
			internalFE.FEserializerTypeEnum = InternalSerializerTypeE.Binary;
			internalFE.FEassemblyFormat = this.m_assemblyFormat;
			internalFE.FEsecurityLevel = this.m_securityLevel;
			ObjectReader objectReader = new ObjectReader(serializationStream, this.m_surrogates, this.m_context, internalFE, this.m_binder);
			objectReader.crossAppDomainArray = this.m_crossAppDomainArray;
			return objectReader.Deserialize(handler, new __BinaryParser(serializationStream, objectReader), fCheck, isCrossAppDomain, methodCallMessage);
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x000F0CE7 File Offset: 0x000EFCE7
		public void Serialize(Stream serializationStream, object graph)
		{
			this.Serialize(serializationStream, graph, null);
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x000F0CF2 File Offset: 0x000EFCF2
		public void Serialize(Stream serializationStream, object graph, Header[] headers)
		{
			this.Serialize(serializationStream, graph, headers, true);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x000F0D00 File Offset: 0x000EFD00
		internal void Serialize(Stream serializationStream, object graph, Header[] headers, bool fCheck)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentNull_WithParamName"), new object[]
				{
					serializationStream
				}));
			}
			InternalFE internalFE = new InternalFE();
			internalFE.FEtypeFormat = this.m_typeFormat;
			internalFE.FEserializerTypeEnum = InternalSerializerTypeE.Binary;
			internalFE.FEassemblyFormat = this.m_assemblyFormat;
			ObjectWriter objectWriter = new ObjectWriter(this.m_surrogates, this.m_context, internalFE);
			__BinaryWriter serWriter = new __BinaryWriter(serializationStream, objectWriter, this.m_typeFormat);
			objectWriter.Serialize(graph, headers, serWriter, fCheck);
			this.m_crossAppDomainArray = objectWriter.crossAppDomainArray;
		}

		// Token: 0x040023B3 RID: 9139
		internal ISurrogateSelector m_surrogates;

		// Token: 0x040023B4 RID: 9140
		internal StreamingContext m_context;

		// Token: 0x040023B5 RID: 9141
		internal SerializationBinder m_binder;

		// Token: 0x040023B6 RID: 9142
		internal FormatterTypeStyle m_typeFormat = FormatterTypeStyle.TypesAlways;

		// Token: 0x040023B7 RID: 9143
		internal FormatterAssemblyStyle m_assemblyFormat;

		// Token: 0x040023B8 RID: 9144
		internal TypeFilterLevel m_securityLevel = TypeFilterLevel.Full;

		// Token: 0x040023B9 RID: 9145
		internal object[] m_crossAppDomainArray;
	}
}
