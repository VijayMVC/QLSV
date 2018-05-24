------------------------------
--Lấy danh sách khoa theo tên khoa
--Lấy toàn bộ danh sách khoa nếu tên khoa rỗng
------------------------------
CREATE PROC GetFacultyList
	@NAME NVARCHAR(200)
AS
IF @NAME = ''
BEGIN
SELECT * FROM FACULTY
END
ELSE
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
IF @NAME = ''
BEGIN
SELECT * FROM GENRE
WHERE FACULTYCODE = @FACULTYCODE
END
ELSE
BEGIN
SELECT * FROM GENRE
WHERE GENRENAME LIKE '%'+@NAME+'%' AND FACULTYCODE = @FACULTYCODE
END