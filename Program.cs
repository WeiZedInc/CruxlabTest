namespace CruxlabTest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			PasswordValidator passwordValidator = new("cruxLabInput.txt");
			passwordValidator.Validate();
			passwordValidator.ShowValidCount();
			Console.ReadLine();
		}
	}
}