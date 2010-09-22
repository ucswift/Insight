using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Files
{
	/// <summary>
	/// This is a three line summary comment
	/// </summary>
	public class TestCSharpFile
	{
		// This is a multi line comment
		// it's a pretty comment with
		// lots of pretty things to say

		/* This is also a multi line comment
		 * and it also has things to say
		 */ 
		
		public void TestOne()
		{
			int i = 0;	// This is an example of a comment and a source line
			int t = 0; /* This is another example of a comment and a source line */
			int z = 0;

			// Single line comment
			i = 50;
			t = 100;

			z = i*t;
			z = z%10;
		}
	}	// this is an example of a comment and a synatx line
} /* this is an example of a comment and a syntax line */