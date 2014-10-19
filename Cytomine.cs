using System;
using System.Text;
using System.Security.Cryptography;

public class Cytomine
{

	private readonly string host;
	private readonly string publicKey;
	private readonly string privateKey;

	private string path;

	public static string VERSION = "0.01";

	public string Path
	{
	    get 
	    {
	       return path; 
	    }
	    set 
	    {
	       path = value; 
	    }
	}

    public Cytomine(string host, string publicKey, string privateKey) {
		this.host = host;
		this.publicKey = publicKey;
		this.privateKey = privateKey;
	}

    public void PrintConnectionInfo() {
        Console.WriteLine(this.host + " => " + this.publicKey + " " +this.privateKey);
    }

    public static void Main() {
        Cytomine cytomine = new Cytomine("http://localhost:8080","pubkey","privkey");
		cytomine.PrintConnectionInfo(); 
		Project project = cytomine.GetProject(123);
		Console.WriteLine(project);

		ProjectCollection projects = cytomine.GetProjects();
		Console.WriteLine(projects[0]);
		Console.WriteLine(projects["NEO04"]);
		cytomine.AddProject("NEO13");
		//cytomine.DeleteProject(123);
    }

	/*
	 * <summary>Get a project</summary>
	 * <param name="id">The project id</param>
	 * <returns>Project with this id</returns>
	 */
	public Project GetProject(long id) {
		Project project = new Project("NEO04");
		project.setCreatedFromLong(1365419405324);
		return project;
	}

	public ProjectCollection GetProjects() {
		ProjectCollection projects = new ProjectCollection();
		projects.add(new Project("NEO04"));
		return projects;
	}

	public Project AddProject(String name) {
		Project project = new Project(name);
		return AddModel(project);
	}

	public Object DeleteProject (long id)
	{
		throw new NotImplementedException();
	}


	private T AddModel<T>(T model) where T : Model
	{
		Console.WriteLine("Add model " + model);
		Console.WriteLine("do request...");
		return model;
	}


	public static string CreateSignature (string publicKey, string privateKey, string messageToSign)
	{
			/**publicKey=89279a28-9907-4014-9cb8-dde6682f1435
			privateKey=7af025b6-0687-4909-b4d2-f5df53777532
			messageToSign=GET


			Fri, 26 Sep 2014 08:19:32 +0000
			/api/storage/17763541.json
			signature=TjwiXi9xGyr34NjA6TzsBLBAPrg=
			authorization=CYTOMINE 89279a28-9907-4014-9cb8-dde6682f1435:TjwiXi9xGyr34NjA6TzsBLBAPrg=
			**/


			//string publicKey="89279a28-9907-4014-9cb8-dde6682f1435";
			//string privateKey="7af025b6-0687-4909-b4d2-f5df53777532";
			//string messageToSign = "GET\n\n\nFri, 26 Sep 2014 08:19:32 +0000\n/api/storage/17763541.json";

		HMACSHA1 hmacsha1 = new HMACSHA1();
		string secretKey = privateKey;
		string content = messageToSign;

		byte[] secretKeyBArr = Encoding.UTF8.GetBytes(secretKey);
		byte[] contentBArr = Encoding.UTF8.GetBytes(content);

		hmacsha1.Key = secretKeyBArr;
		byte[] final = hmacsha1.ComputeHash(contentBArr);
		string signature = System.Convert.ToBase64String(final);
		//Console.WriteLine (signature + " ======== " +"TjwiXi9xGyr34NjA6TzsBLBAPrg=");
	
		String authorization = "CYTOMINE " + publicKey + ":" + signature;

		return authorization;
	}
}
