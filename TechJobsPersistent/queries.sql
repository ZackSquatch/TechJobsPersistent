--Part 1
-- Jobs Table:
    -- Id -> INT
    -- Name -> LONGTEXT
    -- EmployerId -> INT

--Part 2
SELECT Name
FROM techjobs.Employers
WHERE (Location = "Saint Louis, MO");

--Part 3
SELECT techjobs.Skills.Name, techjobs.Skills.Description
FROM techjobs.Skills
INNER JOIN techjobs.jobSkills ON techjobs.Skills.Id = techjobs.jobSkills.SkillId
WHERE techjobs.jobSkills.jobId IS NOT NULL
ORDER BY techjobs.Skills.Name ASC;