namespace WaveTech.Insight.Model
{
	public class SourceLine
	{
		public LineTypes LineType { get; set; }
		public int SourceLength { get; set; }
		public int CommentLength { get; set; }


		public int TotalLength
		{
			get { return SourceLength + CommentLength; }
		}
	}
}