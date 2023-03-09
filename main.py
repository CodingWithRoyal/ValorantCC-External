import tkinter as tk
from tkinter import ttk
from PIL import Image, ImageTk

##################################################
# TKinter not supporting transparent background  #
# So I am using a trick :D (Line 25 and Line 19) #
##################################################

imgSize = 10

# load image from a local path
image = Image.open("crosshair.png")
image = image.resize((imgSize,imgSize))

# define a window
root = tk.Tk()
root.attributes('-topmost', True) # make the window stay always on top
root.wm_attributes("-transparentcolor", "black") # mention label bg color to consider as transparentColor
root.overrideredirect(True) # remove title bar, border, and windows box

imageTk = ImageTk.PhotoImage(image)

# add a image to a label and display it on window
label = ttk.Label(root, image=imageTk, background='black') # set a bg color here
# pack label widget into window
label.pack()

# place the label in the center of the screen
root.geometry("+{}+{}".format(
    int(root.winfo_screenwidth() / 2 - (imgSize/2)),
    int(root.winfo_screenheight() / 2 - (imgSize/2))
))

root.mainloop()