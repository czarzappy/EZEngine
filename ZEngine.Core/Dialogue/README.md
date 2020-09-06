# Dialogue System

## Goals
- Easy for designers and writers to write and review Dialogue

## Scene Types

- *CUTSCENE
- *OPERATION


- *ENDSCENE



Metadata:
- *TITLE
- *SUBTITLE
- *BG
- *BGM

### Scene Type: CUTSCENE

- Dialogue Definition - multiple

### Scene Type: OPERATION

Metadata:
- *BG
- *BGM
- *TITLE - Fade in Name of the mission
- *SUBTITLE - Fade in Name of the mission
- *INITIAL_PATIENT: (PATIENTID)[,(PATIENTID)...] - Patient IDs
    MUST Define at least one
- *NEW_PATIENT: (ID:Number) - New Patient Definition, multiple
    optional
- *PATIENT: (ID:Number) - Patient Definition
    MUST Define at least one


### New Patient Definition

Metadata:
- *APPEAR_TYPE: [APPEARANCE_CONDITION_TYPE], [APPEARANCE_CONDITION_PARAMETERS]
    MUST
- Dialogue Definition - multiple

### Patient Definition

Metadata:
- *PATIENT: (ID:number)
    MUST
- *NAME - Descriptor
    MUST
- *VITALS- two number, max, current
    MUST
- *PHASE - n number of Phase Definitions
    At least one phase



### Scene 
Metadata:
- *PHASE - multiple

### Phase Definition
*PHASE[: SCENENAME]

Metadata:
- Dialogue:
- Affliction Definitions
- *NEWPATIENT: (PATIENTID:number) - Spawns a new patient of ID


### Dialogue

- *[DEVNAME]
- *PATIENT: Patient in context

- CHOICE

### Afflictions Types

- *WOUND_GLASS
- *WOUND_CUT_LARGE
- *WOUND_CUT_SMALL
- *WOUND_STEEL_SMALL
- *SCALPEL: 1

*AFFLICTION_TYPE[: TYPE[,TYPE...]|,AMOUNT]

Affliction Format:
- : - Denotes specific afflictions of a given Type
- , - Denotes number of afflictions of a given Type




