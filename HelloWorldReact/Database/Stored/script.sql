CREATE PROCEDURE [dbo].[GET_SUBJECT_BY_SPECIALITY]
	@specialitycode varchar(10),
	@leveleducation varchar(10),
	@semester int 
AS
BEGIN
	IF (@specialitycode <> '')
	BEGIN
		SELECT * FROM TABLESUBJECT tb WHERE tb.LEVELEDUCATIONCODE =@leveleducation AND tb.SUBJECTTYPE = 0 OR tb.SPECIALITYCODE = @specialitycode Order by CODEVIEW
	END
	IF(@semester IS NOT NULL)
	BEGIN
		SELECT * FROM TABLESUBJECT tb WHERE tb.LEVELEDUCATIONCODE = @leveleducation AND tb.EXPECTEDSEMESTER = @semester Order by CODEVIEW
	END
END