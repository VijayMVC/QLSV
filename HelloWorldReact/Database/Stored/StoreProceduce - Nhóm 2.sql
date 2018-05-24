------------------------------
--Lấy danh sách khoa theo tên khoa
--Lấy toàn bộ danh sách khoa nếu tên khoa rỗng
------------------------------
CREATE PROC GetFacultyList
	@NAME NVARCHAR(200)
AS
BEGIN
SELECT * FROM FACULTY
WHERE FACULTYNAME LIKE '%'+@NAME+'%'
END

------------------------------
--Lấy thông tin khoa theo mã khoa
------------------------------
CREATE PROC GetFaculty
	@CODEVIEW VARCHAR(20)
AS
BEGIN
SELECT * FROM FACULTY
WHERE CODEVIEW = @CODEVIEW
END

------------------------------
--Thêm mới một khoa
------------------------------
CREATE PROC CreateFaculty
	@CODE VARCHAR(10),
	@CODEVIEW VARCHAR(20),
	@NAME NVARCHAR(200),
	@DESC NVARCHAR(200)
AS
BEGIN
INSERT INTO FACULTY
VALUES (@CODE, @CODEVIEW, @NAME, @DESC)
END

------------------------------
--Xóa một khoa
--Cập nhật lại mã khoa ở những bộ môn liên quan
------------------------------
CREATE PROC DeleteFaculty
	@CODEVIEW VARCHAR(20)
AS
BEGIN
DELETE FROM FACULTY WHERE CODEVIEW = @CODEVIEW
END
BEGIN
UPDATE GENRE
SET FACULTYCODE = NULL
WHERE FACULTYCODE = @CODEVIEW
END

------------------------------
--Cập nhật thông tin một khoa
--Cập nhật lại mã khoa ở những bộ môn liên quan
------------------------------
CREATE PROC UpdateFaculty
	@CODEVIEW VARCHAR(20),
	@NEWCODEVIEW VARCHAR(20),
	@NAME NVARCHAR(200),
	@DESC NVARCHAR(200)
AS
BEGIN
UPDATE FACULTY
SET CODEVIEW = @NEWCODEVIEW, FACULTYNAME = @NAME, FACULTYDESCRIPTION = @DESC
WHERE CODEVIEW = @CODEVIEW
END
BEGIN
UPDATE GENRE
SET FACULTYCODE = @NEWCODEVIEW
WHERE FACULTYCODE = @CODEVIEW
END

------------------------------
--Lấy danh sách bộ môn theo tên và mã khoa
--Lấy toàn bộ danh sách bộ môn nếu mã khoa rỗng
------------------------------
CREATE PROC GetGenreList
	@NAME NVARCHAR(100),
	@FACULTYCODE NVARCHAR(20)
AS
IF @FACULTYCODE = ''
BEGIN
SELECT * FROM GENRE
WHERE GENRENAME LIKE '%'+@NAME+'%'
END
ELSE
BEGIN
SELECT * FROM GENRE
WHERE GENRENAME LIKE '%'+@NAME+'%' AND FACULTYCODE = @FACULTYCODE
END

------------------------------
--Lấy thông tin bộ môn theo mã bộ môn
------------------------------

CREATE PROC GetGenre
	@CODEVIEW VARCHAR(20)
AS
BEGIN
SELECT * FROM GENRE
WHERE CODEVIEW = @CODEVIEW
END

------------------------------
--Xóa bộ môn theo mã bộ môn
------------------------------
CREATE PROC DeleteGenre
	@CODEVIEW VARCHAR(20)
AS
BEGIN
DELETE FROM GENRE WHERE CODEVIEW = @CODEVIEW
END
BEGIN
UPDATE TABLESUBJECT
SET GENRECODE = NULL
WHERE GENRECODE = @CODEVIEW
END

------------------------------
--Cập nhật thông tin một bộ môn
--Cập nhật lại mã bộ môn ở những môn học liên quan
------------------------------
CREATE PROC UpdateGenre
	@CODEVIEW VARCHAR(20),
	@NEWCODEVIEW VARCHAR(20),
	@NAME NVARCHAR(200),
	@DESC NVARCHAR(200),
	@FACULTYCODE VARCHAR(20)
AS
BEGIN
UPDATE GENRE
SET CODEVIEW = @NEWCODEVIEW, GENRENAME = @NAME, GENREDESCRIPTION = @DESC, FACULTYCODE = @FACULTYCODE
WHERE CODEVIEW = @CODEVIEW
END
BEGIN
UPDATE TABLESUBJECT
SET GENRECODE = @NEWCODEVIEW
WHERE GENRECODE = @CODEVIEW
END

------------------------------
--Thêm mới một bộ môn
------------------------------
CREATE PROC CreateGenre
	@CODE VARCHAR(10),
	@CODEVIEW VARCHAR(20),
	@NAME NVARCHAR(200),
	@DESC NVARCHAR(200),
	@FACULTYCODE VARCHAR(20)
AS
BEGIN
INSERT INTO GENRE
VALUES (@CODE, @CODEVIEW, @NAME, @DESC, @FACULTYCODE)
END