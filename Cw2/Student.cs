using System;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    public class Student
    {
        [XmlElement(elementName: "index")]
        public string index { get; set; }
        [XmlElement(elementName: "fname")]
        public string fName { get; set; }
        [XmlElement(elementName: "lname")]
        public string lName { get; set; }
        [XmlElement(elementName: "birthdate")]
        public string birthdate { get; set; }
        [XmlElement(elementName: "email")]
        public string email { get; set; }
        [XmlElement(elementName: "mothersName")]
        public string mothersName { get; set; }
        [XmlElement(elementName: "fathersName")]
        public string fathersName { get; set; }
        [XmlElement(elementName: "studiesName")]
        public string studiesName { get; set; }
        [XmlElement(elementName: "studiesMode")]
        public string studiesMode { get; set; }




    }
}