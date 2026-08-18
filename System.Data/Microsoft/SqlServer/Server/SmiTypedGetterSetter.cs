using System;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002E RID: 46
	internal abstract class SmiTypedGetterSetter : ITypedGettersV3, ITypedSettersV3
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000FB RID: 251
		internal abstract bool CanGet { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000FC RID: 252
		internal abstract bool CanSet { get; }

		// Token: 0x060000FD RID: 253 RVA: 0x001D9CF8 File Offset: 0x001D90F8
		public virtual bool IsDBNull(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x001D9D28 File Offset: 0x001D9128
		public virtual SmiMetaData GetVariantType(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x001D9D58 File Offset: 0x001D9158
		public virtual bool GetBoolean(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x001D9D88 File Offset: 0x001D9188
		public virtual byte GetByte(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x001D9DB8 File Offset: 0x001D91B8
		public virtual long GetBytesLength(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x001D9DE8 File Offset: 0x001D91E8
		public virtual int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x001D9E18 File Offset: 0x001D9218
		public virtual long GetCharsLength(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x001D9E48 File Offset: 0x001D9248
		public virtual int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x001D9E78 File Offset: 0x001D9278
		public virtual string GetString(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x001D9EA8 File Offset: 0x001D92A8
		public virtual short GetInt16(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x001D9ED8 File Offset: 0x001D92D8
		public virtual int GetInt32(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x001D9F08 File Offset: 0x001D9308
		public virtual long GetInt64(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x001D9F38 File Offset: 0x001D9338
		public virtual float GetSingle(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x001D9F68 File Offset: 0x001D9368
		public virtual double GetDouble(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x001D9F98 File Offset: 0x001D9398
		public virtual SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x001D9FC8 File Offset: 0x001D93C8
		public virtual DateTime GetDateTime(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x001D9FF8 File Offset: 0x001D93F8
		public virtual Guid GetGuid(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x001DA028 File Offset: 0x001D9428
		public virtual TimeSpan GetTimeSpan(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x001DA058 File Offset: 0x001D9458
		public virtual DateTimeOffset GetDateTimeOffset(SmiEventSink sink, int ordinal)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x001DA088 File Offset: 0x001D9488
		internal virtual SmiTypedGetterSetter GetTypedGetterSetter(SmiEventSink sink, int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x001DA0A8 File Offset: 0x001D94A8
		internal virtual bool NextElement(SmiEventSink sink)
		{
			if (!this.CanGet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x001DA0D8 File Offset: 0x001D94D8
		public virtual void SetDBNull(SmiEventSink sink, int ordinal)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x001DA108 File Offset: 0x001D9508
		public virtual void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x001DA138 File Offset: 0x001D9538
		public virtual void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x001DA168 File Offset: 0x001D9568
		public virtual int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x001DA198 File Offset: 0x001D9598
		public virtual void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x001DA1C8 File Offset: 0x001D95C8
		public virtual int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x001DA1F8 File Offset: 0x001D95F8
		public virtual void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x001DA228 File Offset: 0x001D9628
		public virtual void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x001DA258 File Offset: 0x001D9658
		public virtual void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x001DA288 File Offset: 0x001D9688
		public virtual void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x001DA2B8 File Offset: 0x001D96B8
		public virtual void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x001DA2E8 File Offset: 0x001D96E8
		public virtual void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x001DA318 File Offset: 0x001D9718
		public virtual void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x001DA348 File Offset: 0x001D9748
		public virtual void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x001DA378 File Offset: 0x001D9778
		public virtual void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x001DA3A8 File Offset: 0x001D97A8
		public virtual void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x001DA3D8 File Offset: 0x001D97D8
		public virtual void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x001DA408 File Offset: 0x001D9808
		public virtual void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x001DA438 File Offset: 0x001D9838
		public virtual void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x001DA458 File Offset: 0x001D9858
		internal virtual void NewElement(SmiEventSink sink)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x001DA488 File Offset: 0x001D9888
		internal virtual void EndElements(SmiEventSink sink)
		{
			if (!this.CanSet)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}
