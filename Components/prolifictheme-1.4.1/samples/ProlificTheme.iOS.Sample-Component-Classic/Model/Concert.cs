using System;

namespace ThemeSample {
	public class Concert {
		public string Date { get; private set; }
		public string Title { get; private set; }
		public string Location { get; private set; }

		public Concert ()
		{
			Title = "Sade's Concert";
			Location = "Dave's place";
			Date = "Sat 15th Sep 2012";
		}
	}
}

