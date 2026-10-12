#Bar chart for HCH

#Define the heights of the bars.
heights <- c(602, 501, 1412, 688, 797)

#Define the names for each column.
barNames <-c("Physics", "Chemistry", "Biology", "Engineering", "Mathematics")

#Create the bar graph.
barplot(heights, names.arg = barNames, main = "Distribution of Students by Course", xlab = "Subject", ylab = "Number of Students")
