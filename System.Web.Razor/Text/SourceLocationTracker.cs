using System;
using System.Web.Razor.Parser;

namespace System.Web.Razor.Text
{
	// Token: 0x0200008F RID: 143
	public class SourceLocationTracker
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x000173BC File Offset: 0x000155BC
		public SourceLocationTracker() : this(SourceLocation.Zero)
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000173C9 File Offset: 0x000155C9
		public SourceLocationTracker(SourceLocation currentLocation)
		{
			this.CurrentLocation = currentLocation;
			this.UpdateInternalState();
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x000173DE File Offset: 0x000155DE
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x000173E6 File Offset: 0x000155E6
		public SourceLocation CurrentLocation
		{
			get
			{
				return this._currentLocation;
			}
			set
			{
				if (this._currentLocation != value)
				{
					this._currentLocation = value;
					this.UpdateInternalState();
				}
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00017403 File Offset: 0x00015603
		public void UpdateLocation(char characterRead, char nextCharacter)
		{
			this.UpdateCharacterCore(characterRead, nextCharacter);
			this.RecalculateSourceLocation();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00017414 File Offset: 0x00015614
		public SourceLocationTracker UpdateLocation(string content)
		{
			for (int i = 0; i < content.Length; i++)
			{
				char nextCharacter = '\0';
				if (i < content.Length - 1)
				{
					nextCharacter = content[i + 1];
				}
				this.UpdateCharacterCore(content[i], nextCharacter);
			}
			this.RecalculateSourceLocation();
			return this;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00017460 File Offset: 0x00015660
		private void UpdateCharacterCore(char characterRead, char nextCharacter)
		{
			this._absoluteIndex++;
			if (ParserHelpers.IsNewLine(characterRead) && (characterRead != '\r' || nextCharacter != '\n'))
			{
				this._lineIndex++;
				this._characterIndex = 0;
				return;
			}
			this._characterIndex++;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000174B4 File Offset: 0x000156B4
		private void UpdateInternalState()
		{
			this._absoluteIndex = this.CurrentLocation.AbsoluteIndex;
			this._characterIndex = this.CurrentLocation.CharacterIndex;
			this._lineIndex = this.CurrentLocation.LineIndex;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000174FD File Offset: 0x000156FD
		private void RecalculateSourceLocation()
		{
			this._currentLocation = new SourceLocation(this._absoluteIndex, this._lineIndex, this._characterIndex);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001751C File Offset: 0x0001571C
		public static SourceLocation CalculateNewLocation(SourceLocation lastPosition, string newContent)
		{
			return new SourceLocationTracker(lastPosition).UpdateLocation(newContent).CurrentLocation;
		}

		// Token: 0x04000327 RID: 807
		private int _absoluteIndex;

		// Token: 0x04000328 RID: 808
		private int _characterIndex;

		// Token: 0x04000329 RID: 809
		private int _lineIndex;

		// Token: 0x0400032A RID: 810
		private SourceLocation _currentLocation;
	}
}
