# IOET Challenges
The goal of this repository is to provide solutions for code challenge.

# Exercises
The company ACME offers their employees the flexibility to work the hours they want. They will pay for the hours worked based on the day of the week and time of day, according to the following table:

Monday - Friday
00:01 - 09:00 25 USD
09:01 - 18:00 15 USD
18:01 - 00:00 20 USD

Saturday and Sunday
00:01 - 09:00 30 USD
09:01 - 18:00 20 USD
18:01 - 00:00 25 USD

The goal of this exercise is to calculate the total that the company has to pay an employee, based on the hours they worked and the times during which they worked. The following abbreviations will be used for entering data:

MO: Monday
TU: Tuesday
WE: Wednesday
TH: Thursday
FR: Friday
SA: Saturday
SU: Sunday

Input: the name of an employee and the schedule they worked, indicating the time and hours. This should be a .txt file with at least five sets of data. You can include the data from our two examples below.

Output: indicate how much the employee has to be paid

For example:

Case 1:

INPUT
RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00
OUTPUT:
The amount to pay RENE is: 215 USD

Case 2:

INPUT
ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00
OUTPUT:
The amount to pay ASTRID is: 85 USD

# Solutions

# C#

#.NET version
.NET 6

# Projects

You will find five projects folder:
- Console project which one you can enter the file name to process.
- Business project that contains the logic to solve the alogorithms and the IoC logic.
- DTO project for data transfer objects
- Domain project to store the needed intefaces to implement the solution. 
- Test project which one have covered File Management and Business logic. You can find some file examples too.

# Approach

I have tried to follow SOLID principles. I have created each class providing only one responsability and with specific interfaces. Also I have added IoC mechanisms to avoid know the implementations from other assemblies. I think there is a missing project regarding to IoC to reach 100% with this goal.
Regarding to the previous topic it is the first time I implement those principles so it is possible we have some issues there.
I decided to use regular expresions to validate each file's rows and get that information with Regex and string functions as well.
After that I compare TimeSpan for getting the quantities of hours to pay. Each worker are stored in a dictionary to have a better performance than List or arrays.

# Execution
To execute the program just build the solution and execute the console project.

# Improvement
I think some adjustment could be made in order to avoid performances issues. 
Escenario: You have a file with 10000 rows. In that case, we could have a sort of async mechanism such us store the rows, publish some message to a queue, and solve in other service which listen the event. Return some operationId to the console application and provide some endpoint to ask the result.

