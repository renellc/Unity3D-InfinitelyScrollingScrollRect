# Unity3D-InfiniteScrollRect

A simple solution for creating an infinite scrolling view in Unity3D.

## Demo
![](https://i.imgur.com/kEiAX7q.gif)

## How to use
Download/clone the repo and open it up in Unity3D and choose between a horizontal or vertical scroll view (found in the prefabs folder). Items of the scroll view must have the same height and width. Drag and drop your prefabs/game objects into the ScrollContent game object.

### The ScrollContent component
This component is attached to the ScrollContent game object. This script automatically positions and spaces the items that are children to it along the scroll view. You are able to custom the horizontal and vertical margin, item spacing, and choose the orientation of the scroll view. Note that you can only choose either a horizontal or vertical orientation, not both.

### The InfiniteScroll component
This component is attached to the parent game object that contains the ScrollRect component. There is a field in the inspector for the ScrollContent component, so you must select the ScrollContent game object that contains the ScrollContent component for the field. This script automatically loops the items depending on which way you scroll. The script automatically calculates a transition threshold (point where the items should loop) for both ends, but if you desire to increase the size of this threshold, there is an exposed inspector property named "Out Of Bounds Threshold" that you can customize.

### Any issues?
If there any issues, open up a new issue detailing the problem as well as the steps to recreating this problem.
