using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI
{
	public interface ITest
	{
		Guid TargetId { get; }
	}

	public interface ITestTransient : ITest { }
	public interface ITestScoped : ITest { }
	public interface ITestSingleton : ITest { }

	public class TestInstance : ITestTransient, ITestScoped, ITestSingleton
	{
		public Guid TargetId { get => _targetId; }
		private Guid _targetId;

		public TestInstance()
		{
			_targetId = Guid.NewGuid();
		}
	}
}
