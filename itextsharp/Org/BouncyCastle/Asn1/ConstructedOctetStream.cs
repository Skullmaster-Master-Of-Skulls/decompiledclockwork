using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000451 RID: 1105
	internal class ConstructedOctetStream : BaseInputStream
	{
		// Token: 0x06002550 RID: 9552 RVA: 0x000E20C5 File Offset: 0x000E10C5
		internal ConstructedOctetStream(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000E20DC File Offset: 0x000E10DC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._currentStream == null)
			{
				if (!this._first)
				{
					return 0;
				}
				Asn1OctetStringParser asn1OctetStringParser = (Asn1OctetStringParser)this._parser.ReadObject();
				if (asn1OctetStringParser == null)
				{
					return 0;
				}
				this._first = false;
				this._currentStream = asn1OctetStringParser.GetOctetStream();
			}
			int num = 0;
			for (;;)
			{
				int num2 = this._currentStream.Read(buffer, offset + num, count - num);
				if (num2 > 0)
				{
					num += num2;
					if (num == count)
					{
						break;
					}
				}
				else
				{
					Asn1OctetStringParser asn1OctetStringParser2 = (Asn1OctetStringParser)this._parser.ReadObject();
					if (asn1OctetStringParser2 == null)
					{
						goto Block_6;
					}
					this._currentStream = asn1OctetStringParser2.GetOctetStream();
				}
			}
			return num;
			Block_6:
			this._currentStream = null;
			return num;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000E2174 File Offset: 0x000E1174
		public override int ReadByte()
		{
			if (this._currentStream == null)
			{
				if (!this._first)
				{
					return 0;
				}
				Asn1OctetStringParser asn1OctetStringParser = (Asn1OctetStringParser)this._parser.ReadObject();
				if (asn1OctetStringParser == null)
				{
					return 0;
				}
				this._first = false;
				this._currentStream = asn1OctetStringParser.GetOctetStream();
			}
			int num;
			for (;;)
			{
				num = this._currentStream.ReadByte();
				if (num >= 0)
				{
					break;
				}
				Asn1OctetStringParser asn1OctetStringParser2 = (Asn1OctetStringParser)this._parser.ReadObject();
				if (asn1OctetStringParser2 == null)
				{
					goto Block_5;
				}
				this._currentStream = asn1OctetStringParser2.GetOctetStream();
			}
			return num;
			Block_5:
			this._currentStream = null;
			return -1;
		}

		// Token: 0x04001A21 RID: 6689
		private readonly Asn1StreamParser _parser;

		// Token: 0x04001A22 RID: 6690
		private bool _first = true;

		// Token: 0x04001A23 RID: 6691
		private Stream _currentStream;
	}
}
