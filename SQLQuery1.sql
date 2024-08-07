CREATE PROCEDURE GetStudentByEmail
    @em NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Student WHERE email = @em;
END

--ExEC GetStudentByEmail 'i223734@nu.edu.pk';
--use Ta_LD_managementsystem;
--select * from Student;
--select * from Register_student;
