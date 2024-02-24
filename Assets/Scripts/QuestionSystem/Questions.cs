using System;

[Serializable]
public class Questions
{
    public int Id;
    public string question;
    public string feedback; 

    public Questions(string question, string feedback)  
    {
        Id ++;
        this.question = question;
        this.feedback = feedback;
    }
}
