using NUnit.Framework;
using WaveTech.Insight.Services;

namespace UnitTests.Services
{
	namespace ImageServiceTests
	{
		public class with_the_image_service : FixtureBase
		{
			protected ImageService imageService;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				imageService = new ImageService();
			}
		}

		[TestFixture]
		public class when_generating_an_image : with_the_image_service
		{
			[Test]
			public void should_not_be_null()
			{
				imageService.GenerateImage("S:\\Test.png");
			}
		}
	}
}
